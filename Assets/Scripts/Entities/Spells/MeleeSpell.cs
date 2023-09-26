using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeSpell : MonoBehaviour, IMeleeSpell
{
    #region PRIVATE_PROPERTIES
    [SerializeField] protected float lifetime = 3;
    protected Collider col;
    protected Rigidbody rb;
    protected IRune owner;
    [SerializeField] protected LayerMask hittableMask;
    #endregion

    #region I_MELEESPELL_PROPERTIES
    public float LifeTime => lifetime;
    public Collider Collider => col;
    public Rigidbody Rb => rb;
    public IRune Owner => owner;
    #endregion

    #region I_MELEESPELL_PROPERTIES

    public void Init()
    {
        if (col != null)
        {
            col.isTrigger = true;
        }

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    public void InitSound()
    {
        SpellSound spellSound = GetComponent<SpellSound>();
        EventQueueManager.instance.AddCommand(new CmdPlaySound(spellSound));
    }

    public void Die() => Destroy(this.gameObject);

    public void SetOwner(IRune owner) => this.owner = owner;
    #endregion

    #region UNITY_EVENTS
    private void OnDestroy()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter against {other.name}");
        if (owner.Player.CompareTag(other.tag)) return;

        if (((1 << other.gameObject.layer) & hittableMask) != 0)
        {
            if (other.GetComponent<IDamageable>() != null)
                EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Actor>(), owner.RuneStats.Damage));
            else if (other.GetComponent<Body>() != null)
                EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Body>().Actor, owner.RuneStats.Damage));
        }
    }

    protected void CollideWithActors()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Enemy"));

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, layerMask);
        foreach (Collider other in colliders)
        {
            if (owner.Player.CompareTag(other.tag)) continue;

            if (((1 << other.gameObject.layer) & hittableMask) != 0)
            {
                Debug.Log($"About to deal damage to {other.name}");
                if (other.GetComponent<IDamageable>() != null)
                    EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Actor>(), owner.RuneStats.Damage));
                else if (other.GetComponent<Body>() != null)
                    EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Body>().Actor, owner.RuneStats.Damage));
            }
        }
    }

    void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        gameObject.layer = LayerMask.NameToLayer("Spell");

        Init();

        InitSound();

        CollideWithActors();
    }

    void Update()
    {

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Die();
    }
    #endregion
}
