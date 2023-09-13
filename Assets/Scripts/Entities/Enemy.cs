using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private GameObject _player;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _sightRange, _attackRange;
    private bool _playerInSight, _playerInAttack;

    new void Awake()
    {
        base.Awake();
        if (_player == null) _player = GameObject.Find("Player");
        EventsManager.instance.OnGameOver += OnGameOver;
    } 


    new void Update()
    {
        if (isDead) return;
        base.Update();
        _playerInSight = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);
        _playerInAttack = Physics.CheckSphere(transform.position, _attackRange, _playerLayer);

        if (!_playerInSight && !_playerInAttack) Patrol();
        if (_playerInSight && !_playerInAttack) Chase();
        if (_playerInSight && _playerInAttack) Attack();
    }

    private void Patrol()
    {

    }

    private void Chase()
    {
        agent.SetDestination(_player.transform.position);
    }

    private void Attack()
    {
        if (_player.GetComponent<IDamageable>() != null && _player.GetComponent<IDamageable>().IsDead == true) return;

        agent.SetDestination(transform.position);
        basicAttack.ShootAtDirection(_player.transform.position);
    }

    private void OnGameOver(bool isVictory)
    {
        if (!isVictory) animator.Play("Victory");
    }

    public override void Die()
    {
        EventsManager.instance.EventGameOver(true);
        base.Die();
    }
}
