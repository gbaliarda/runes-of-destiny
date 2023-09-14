using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class FrostBall : MonoBehaviour, IFrostBall
{
    #region PRIVATE_PROPERTEIS
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _lifetime = 3;
    private Collider _collider;
    private Rigidbody _rigidbody;
    private IRune _owner;
    [SerializeField] private LayerMask _hittableMask;
    #endregion

    #region I_FROSTBALL_PROPERTIES
    public float Speed => _speed;
    public float LifeTime => _lifetime;
    public Collider Collider => _collider;
    public Rigidbody Rb => _rigidbody;
    public IRune Owner => _owner;
    #endregion

    #region I_FROSTBALL_PROPERTIES

    public void Init()
    {
        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void InitSound()
    {
        FrostBallSound frostBallSound = GetComponent<FrostBallSound>();
        EventQueueManager.instance.AddCommand(new CmdPlaySound(frostBallSound));
    }

    public void Travel()
    {
        EventQueueManager.instance.AddCommand(new CmdMoveTowardsDirection(transform, transform.forward, Owner.RuneStats.Speed));
    }

    public void Die() => Destroy(this.gameObject);

    public void SetOwner(IRune owner) => _owner = owner;
    #endregion

    #region UNITY_EVENTS
    private void OnDestroy()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_owner.Player.CompareTag(other.tag) || other.name.Equals("FrostBall(Clone)")) return;
       
        if (((1 << other.gameObject.layer) & _hittableMask) != 0)
        {
            if (other.GetComponent<IDamageable>() != null)
                EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Actor>(), _owner.RuneStats.Damage));
            else if (other.GetComponent<Body>() != null)
                EventQueueManager.instance.AddCommand(new CmdApplyDamage(other.GetComponent<Body>().Actor, _owner.RuneStats.Damage));
            Die();
        }
    }
    void Start() 
    { 
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

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

        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0) Die();
    }
    #endregion
}
