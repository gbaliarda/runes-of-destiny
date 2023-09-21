using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(NavMovementController), typeof(AttackController))]
[RequireComponent(typeof(Animator))]
public class Character : Actor
{
    [SerializeField] protected CharacterStats characterStats;
    [SerializeField] protected int mana;
    protected NavMeshAgent agent;
    protected NavMovementController movementController;
    protected AttackController attackController;
    protected Animator animator;
    public int Mana => mana;

    #region UNITY_EVENTS

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = characterStats.MovementSpeed;
        agent.autoBraking = false;
        agent.angularSpeed = 0;
        agent.acceleration = 99999;

        animator = GetComponent<Animator>();

        movementController = GetComponent<NavMovementController>();
        attackController = GetComponent<AttackController>();

    }
    protected new void Start()
    {
        base.stats = characterStats;
        base.Start();

        mana = characterStats.MaxMana;

        StartCoroutine(ManaRegenCoroutine());
    }

    protected void Update()
    {
        if (isDead) return;
        FaceTarget();
        SetAnimations();
    }
    #endregion

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

    public virtual void AbilityCasted(int runeIndex)
    {
        SpendMana(attackController.Runes[runeIndex].RuneStats.ManaCost);
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
            }
        }
    }
}
