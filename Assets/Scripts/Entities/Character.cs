using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Character : Actor, IMana, IBuffable
{
    [SerializeField] private CharacterStats baseStats;
    [SerializeField] protected int mana;
    [SerializeField] private List<AudioClip> _takeDamageClip;
    protected ManaPotionController manaPotionController;
    protected NavMeshAgent agent;
    protected NavMovementController movementController;
    protected AttackController attackController;
    protected Animator animator;
    protected List<IBuff> buffs;
    protected CharacterStats characterStats;
    public int Mana => mana;
    public int MaxMana => baseStats.MaxMana;
    public List<IBuff> Buffs => buffs;
    public Dictionary<IBuff, CmdBuff> Buffs2 => Buffs2;
    public CharacterStats CharacterStats => characterStats;

    #region UNITY_EVENTS

    protected void Awake()
    {
        characterStats = Instantiate(baseStats);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = characterStats.MovementSpeed;
        agent.autoBraking = false;
        agent.angularSpeed = 0;
        agent.acceleration = 99999;

        animator = GetComponent<Animator>();

        movementController = GetComponent<NavMovementController>();
        attackController = GetComponent<AttackController>();

        manaPotionController = GetComponent<ManaPotionController>();
    }
    protected new void Start()
    {
        base.stats = characterStats;
        base.Start();
        if (buffs == null) buffs = new List<IBuff>();

        mana = characterStats.MaxMana;
        if (movementController != null) movementController.SetSpeed(characterStats.MovementSpeed);

        StartCoroutine(ManaRegenCoroutine());
        StartCoroutine(HealthRegenCoroutine());
    }

    protected void Update()
    {
        if (isDead) return;
        FaceTarget();
        SetAnimations();
    }
    #endregion

    public void AddBuff(IBuff buff)
    {
        if (isDead) return;
        characterStats.AddStats(buff.Owner.BuffStats);
        if (this is Player && buff is HealthBuff)
        {
            EventsManager.instance.EventTakeDamage(life);
            EventsManager.instance.EventTargetHealthChange(gameObject.GetInstanceID(), life, MaxLife);
            EventsManager.instance.EventUpdateCharacterStats();
        }
        buffs.Add(buff);
    }

    public void RemoveBuff(IBuff buff)
    {
        if (isDead) return;
        characterStats.RemoveStats(buff.Owner.BuffStats);
        if (buff.Owner.BuffStats.MaxLife > 0)
        {
            if (life > characterStats.MaxLife) life = characterStats.MaxLife;
            if (this is Player)
            {
                EventsManager.instance.EventTakeDamage(life);
                EventsManager.instance.EventTargetHealthChange(gameObject.GetInstanceID(), life > 0 ? life : 0, MaxLife);
                EventsManager.instance.EventUpdateCharacterStats();
            }
        }
        buffs.Remove(buff);
    }

    private void FaceTarget()
    {
        if (agent.velocity != Vector3.zero) EventQueueManager.instance.AddCommand(new CmdChangeRotation(transform, (agent.destination - transform.position).normalized));
    }

    private void SetAnimations()
    {
        if (agent.velocity == Vector3.zero) EventQueueManager.instance.AddCommand(new CmdPlayAnimation(animator, "Idle"));
        else EventQueueManager.instance.AddCommand(new CmdPlayAnimation(animator, "Walk")); ;
    }

    public virtual void SpendMana(int manaCost)
    {
        if (isDead) return;
        mana -= manaCost;
        if (mana < 0) mana = 0;
    }

    public virtual void GetMana(int newMana)
    {
        if (isDead) return;
        mana += newMana;
        if (mana > characterStats.MaxMana) mana = characterStats.MaxMana;
    }

    public override int TakeDamage(DamageStatsValues damage)
    {
        if (isGameOver) return life;
        if (isDead) return life;
        int PhysicalDamage = Mathf.RoundToInt(damage.PhysicalDamage * (1 - characterStats.Armor/(characterStats.Armor + 5000f)));
        int FireDamage = Mathf.RoundToInt(damage.FireDamage*(1 - characterStats.FireResistance / 100f));
        int WaterDamage = Mathf.RoundToInt(damage.WaterDamage * (1 - characterStats.WaterResistance / 100f));
        int LightningDamage = Mathf.RoundToInt(damage.LightningDamage * (1 - characterStats.LightningResistance / 100f));
        int VoidDamage = Mathf.RoundToInt(damage.VoidDamage * (1 - characterStats.VoidResistance / 100f));
        Debug.Log($"Taking {PhysicalDamage} as physical, {FireDamage} as fire, {WaterDamage} as water, {LightningDamage} as lightning, {VoidDamage} as void");
        
        int chanceToMiss = Random.Range(1, 100);
        if (chanceToMiss < characterStats.EvasionChance)
        {
            Debug.Log("Damage evaded");
            return life;
        }

        int chanceToBlock = Random.Range(1, 100);
        float damageModifier = 1;
        if (chanceToBlock < characterStats.BlockSpellChance)
        {
            Debug.Log("Damage blocked");
            damageModifier = 1 - characterStats.DamageBlockedAmount / 100f;
        }

        life -= Mathf.RoundToInt(damageModifier * (PhysicalDamage + FireDamage + WaterDamage + LightningDamage + VoidDamage));

        Debug.Log($"New life is {life}");

        EventsManager.instance.EventTargetHealthChange(gameObject.GetInstanceID(), life > 0 ? life : 0, MaxLife);
        if (this is Player) EventsManager.instance.EventTakeDamage(life);
        if (life <= 0) Die();
        if (_takeDamageClip != null && _takeDamageClip.Count > 0) audioSource.PlayOneShot(_takeDamageClip[Random.Range(0, _takeDamageClip.Count)]);
        return life;
    }

    public override void Die()
    {
        if (movementController != null) movementController.Move(transform.position);
        EventQueueManager.instance.AddCommand(new CmdPlayAnimation(animator, "Dead"));
        isDead = true;
        Destroy(gameObject, 5f);
    }

    protected virtual IEnumerator ManaRegenCoroutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);
            if (mana < characterStats.MaxMana)
            {
                new CmdGetMana(this, characterStats.ManaRegen).Execute();
                if (mana >  characterStats.MaxMana) mana = characterStats.MaxMana;
                if (this is Player) EventsManager.instance.EventSpendMana(mana);
            }
        }
    }

    protected virtual IEnumerator HealthRegenCoroutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);
            if (life < characterStats.MaxLife)
            {
                new CmdHealDamage(this, characterStats.HealthRegen).Execute();
                EventsManager.instance.EventTargetHealthChange(gameObject.GetInstanceID(), life, MaxLife);
                if (this is Player) EventsManager.instance.EventTakeDamage(life);
            }
        }
    }
}
