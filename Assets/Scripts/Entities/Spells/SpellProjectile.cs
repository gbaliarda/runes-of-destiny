using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class SpellProjectile : MonoBehaviour, ISpellProjectile
{
    #region PRIVATE_PROPERTEIS
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float lifetime = 3;
    protected Collider col;
    protected Rigidbody rb;
    protected RangedRune owner;
    [SerializeField] protected LayerMask hittableMask;
    #endregion

    #region I_SPELLPROJECTILE_PROPERTIES
    public float Speed => speed;
    public float LifeTime => lifetime;
    public Collider Collider => col;
    public Rigidbody Rb => rb;
    public RangedRune Owner => owner;
    #endregion

    #region I_SPELLPROJECTILE_PROPERTIES

    public void Init()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void InitSound()
    {
        SpellSound spellSound = GetComponent<SpellSound>();
        EventQueueManager.instance.AddCommand(new CmdPlaySound(spellSound));
    }

    public void Travel()
    {
        EventQueueManager.instance.AddCommand(new CmdMoveTowardsDirection(transform, transform.forward, Owner.RangedRuneStats.Speed));
    }

    public void Die() {
        Destroy(this.gameObject);
    }

    public void SetOwner(RangedRune owner) => this.owner = owner;
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
            {
                EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Actor>(), owner.RuneStats.Damage));
                if (other.GetComponent<Actor>().IsDead) return;
            }
            else if (other.GetComponent<Body>() != null)
            {
                EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Body>().Actor, owner.RuneStats.Damage));
                if (other.GetComponent<Body>().Actor.IsDead) return;
            }
            Die();
        }
    }
    void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        gameObject.layer = LayerMask.NameToLayer("Spell");

        Init();

        InitSound();
    }

    void Update()
    {
        Travel();


        int groundLayerMask = LayerMask.GetMask("Ground");

        RaycastHit groundHit;
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, Mathf.Infinity, groundLayerMask))
        {
            Vector3 newPosition = new(transform.position.x, groundHit.point.y + 2f, transform.position.z);
            transform.position = newPosition;
        }

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Die();
    }
    #endregion
}
