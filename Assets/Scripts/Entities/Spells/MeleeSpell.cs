using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
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
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void InitSound()
    {
        CosmicStarSound frostBallSound = GetComponent<CosmicStarSound>();
        EventQueueManager.instance.AddCommand(new CmdPlaySound(frostBallSound));
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

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Die();
    }
    #endregion
}
