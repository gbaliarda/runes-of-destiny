using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : Actor
{
    [SerializeField] protected RangedRune basicAttack;
    [SerializeField] protected CharacterStats characterStats;
    [SerializeField] protected float mana;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected bool isDead = false;
    public float Mana => mana;

    #region UNITY_EVENTS

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = characterStats.MovementSpeed;
        agent.autoBraking = false;
        agent.angularSpeed = 0;
        agent.acceleration = 99999;

        animator = GetComponent<Animator>();

    }
    void Start()
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

    public void SpendMana(int manaCost)
    {
        mana -= manaCost;
        if (mana < 0) mana = 0;
    }

    public override void Die()
    {
        animator.Play("Dead");
        isDead = true;
        StartCoroutine(RemoveCharacter());
    }

    private IEnumerator ManaRegenCoroutine()
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

    private IEnumerator RemoveCharacter()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
