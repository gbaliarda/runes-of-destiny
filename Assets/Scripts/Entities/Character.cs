using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Character : Actor
{
    [SerializeField] private CharacterStats baseStats;
    [SerializeField] protected int mana;
    protected ManaPotionController manaPotionController;
    protected NavMeshAgent agent;
    protected NavMovementController movementController;
    protected AttackController attackController;
    protected Animator animator;
    protected List<Buff> buffs;
    protected CharacterStats characterStats;
    public int Mana => mana;
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
        if (buffs == null) buffs = new List<Buff>();

        mana = characterStats.MaxMana;

        StartCoroutine(ManaRegenCoroutine());
        StartCoroutine(HealthRegenCoroutine());
    }

    protected void Update()
    {
        if (isDead) return;
        FaceTarget();
        SetAnimations();
        UpdateBuffs();
    }
    #endregion

    private void UpdateBuffs()
    {
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            IBuff buff = buffs[i];
            buff.ReduceDuration(Time.deltaTime);
            if (buff.Duration <= 0)
            {
                characterStats.RemoveStats(buff.Owner.BuffStats);
                if (buff.Owner.BuffStats.MaxLife > 0)
                {
                    if (life > characterStats.MaxLife) life = characterStats.MaxLife;
                    if (this is Player) EventsManager.instance.EventTakeDamage(life);
                }
                buffs.RemoveAt(i);
            }
        }
    }

    public void AddBuff(Buff buff)
    {
        characterStats.AddStats(buff.Owner.BuffStats);
        if (this is Player && buff is HealthBuff) EventsManager.instance.EventTakeDamage(life);
        buffs.Add(buff);
    }

    private void FaceTarget()
    {
        if (agent.velocity != Vector3.zero) new CmdChangeRotation(transform, (agent.destination - transform.position).normalized).Execute();
    }

    private void SetAnimations()
    {
        if (agent.velocity == Vector3.zero) animator.Play("Idle");
        else animator.Play("Walk");
    }

    public virtual void SpendMana(int manaCost)
    {
        mana -= manaCost;
        if (mana < 0) mana = 0;
    }

    public virtual void GetMana(int newMana)
    {
        mana += newMana;
        if (mana > characterStats.MaxMana) mana = characterStats.MaxMana;
    }

    public virtual void AbilityCasted(int runeIndex)
    {
        if (attackController == null) return;
        SpendMana(attackController.Runes[runeIndex].RuneStats.ManaCost);
    }

    public override int TakeDamage(DamageStatsValues damage)
    {
        int PhysicalDamage = Mathf.RoundToInt(damage.PhysicalDamage * (1 - characterStats.Armor/(characterStats.Armor + 5000f)));
        int FireDamage = Mathf.RoundToInt(damage.FireDamage*(1 - characterStats.FireResistance / 100f));
        int WaterDamage = Mathf.RoundToInt(damage.WaterDamage * (1 - characterStats.WaterResistance / 100f));
        int LightningDamage = Mathf.RoundToInt(damage.LightningDamage * (1 - characterStats.LightningResistance / 100f));
        int VoidDamage = Mathf.RoundToInt(damage.VoidDamage * (1 - characterStats.VoidResistance / 100f));
        
        int chanceToMiss = Random.Range(1, 100);
        if (chanceToMiss < characterStats.EvasionChance) return life;

        int chanceToBlock = Random.Range(1, 100);
        float damageModifier = 1;
        if (chanceToBlock < characterStats.BlockSpellChance) damageModifier = 1 - characterStats.DamageBlockedAmount / 100f;

        life -= Mathf.RoundToInt(damageModifier * (PhysicalDamage + FireDamage + WaterDamage + LightningDamage + VoidDamage));

        if (this is Player) EventsManager.instance.EventTakeDamage(life);
        if (life <= 0) Die();
        return life;
    }

    public override void Die()
    {
        animator.Play("Dead");
        isDead = true;
        Destroy(gameObject, 5f);
    }

    protected virtual IEnumerator ManaRegenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (mana < characterStats.MaxMana)
            {
                mana += characterStats.ManaRegen;
                if (mana >  characterStats.MaxMana) mana = characterStats.MaxMana;
                if (this is Player) EventsManager.instance.EventSpendMana(mana);
            }
        }
    }

    protected virtual IEnumerator HealthRegenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (life < characterStats.MaxLife)
            {
                life += characterStats.HealthRegen;
                if (life > characterStats.MaxLife) life = characterStats.MaxLife;
                if (this is Player) EventsManager.instance.EventTakeDamage(life);
            }
        }
    }
}
