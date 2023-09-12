using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(NavMeshAgent))]
[RequireComponent (typeof(EventQueueManager))]
public class Character : Actor
{
    [SerializeField] private RangedRune _basicAttack;
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private float _mana;
    [SerializeField] private ParticleSystem _clickEffect;
    private NavMeshAgent _agent;
    private Animator _animator;
    private RaycastHit _hit;
    private string groundTag = "Ground";
    public float Mana => _mana;


    #region KEY_BINDINGS
    [SerializeField] private KeyCode _move = KeyCode.Mouse1;
    [SerializeField] private KeyCode _shootAttack = KeyCode.Q;

    #endregion


    #region UNITY_EVENT

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _characterStats.MovementSpeed;
        _agent.autoBraking = false;
        _agent.angularSpeed = 0;
        _agent.acceleration = 99999;

        _animator = GetComponent<Animator>();

    }
    void Start()
    {
        base.stats = _characterStats;
        base.Start();

        _mana = _characterStats.MaxMana;

        StartCoroutine(ManaRegenCoroutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(_shootAttack)) 
        {
            _basicAttack.Shoot(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) EventsManager.instance.EventGameOver(true);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EventsManager.instance.EventGameOver(false);

        if (Input.GetKeyDown(_move))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit) && _hit.collider.CompareTag(groundTag))
            {
                new CmdMoveToClick(_agent, _hit).Execute();
                if(_clickEffect != null) {
                    GameObject effectContainer = new GameObject("ClickEffectContainer");

                    effectContainer.transform.position = _hit.point + new Vector3(0, 0.1f, 0);

                    ParticleSystem effectInstance = Instantiate(_clickEffect, effectContainer.transform);

                    effectInstance.transform.localPosition = Vector3.zero;

                    Destroy(effectContainer, 2.0f);
                }
            }
        }

        FaceTarget();
        SetAnimations();
    }
    #endregion

    private void FaceTarget()
    {
        if (_agent.velocity != Vector3.zero)
        {
            Vector3 direction = (_agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void SetAnimations()
    {
        if (_agent.velocity == Vector3.zero)
        {
            _animator.Play("Idle");
        } else
        {
            _animator.Play("Walk");
        }
    }

    public void SpendMana(int manaCost)
    {
        _mana -= manaCost;
        if (_mana < 0) _mana = 0;
    }

    private IEnumerator ManaRegenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (_mana < _characterStats.MaxMana)
            {
                _mana += _characterStats.ManaRegen;
                if (_mana >  _characterStats.MaxMana) _mana = _characterStats.MaxMana;
            }
        }
    }
}
