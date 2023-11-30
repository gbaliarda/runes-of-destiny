using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private GameObject _player;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _sightRange, _attackRange;
    [SerializeField] private bool _isFinalBoss;
    [SerializeField] private GameObject _pickablePrefab;
    [SerializeField] private GameObject _pickableDropPrefab;
    private Database _database;
    private bool _playerInSight, _playerInAttack;
    [SerializeField] private int[] _droppableItemIds;
    private Dictionary<int, double> _lootTable;

    new void Start()
    {
        base.Start();

        _database = new Database();

        _lootTable = new Dictionary<int, double>();
        foreach (int itemId in _droppableItemIds)
        {
            ItemData itemData = _database.GetItem(itemId);
            if (itemData != null)
            {
                _lootTable.Add(itemId, itemData.DropChance);
            }
        }

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
        if (_player.GetComponent<IDamageable>() != null && _player.GetComponent<IDamageable>().IsDead == true) return;
        if (movementController != null) movementController.Move(_player.transform.position);
    }

    private void FaceEnemy()
    {
        EventQueueManager.instance.AddCommand(new CmdChangeRotation(transform, (_player.transform.position - transform.position).normalized));
    }

    private void Attack()
    {
        movementController.Move(transform.position);
        if (attackController == null) return;
        if (_player.GetComponent<IDamageable>() != null && _player.GetComponent<IDamageable>().IsDead == true) return;
        FaceEnemy();
        if (attackController.Runes[0].CooldownLeft > 0) return;
        if (attackController.Runes[0].RuneStats.ManaCost > mana) return;

        
        EventQueueManager.instance.AddCommand(new CmdSpendMana(this, attackController.Runes[0].RuneStats.ManaCost));
        attackController.Runes[0].SetCooldown(attackController.Runes[0].RuneStats.Cooldown);

        attackController.Attack(0, _player.transform.position);
    }

    private void OnGameOver(bool isVictory)
    {
        base.OnGameOver(isVictory);
        if (!isVictory) EventQueueManager.instance.AddCommand(new CmdPlayAnimation(animator, "Victory"));
    }

    public override void Die()
    {
        if (_isFinalBoss) EventsManager.instance.EventGameOver(true);
        GameObject pickableContainer = GameObject.Find("Pickables");
        GameObject dropContainer = GameObject.Find("Drops");
        float random = UnityEngine.Random.Range(0f, 1f);

        foreach (KeyValuePair<int, double> entry in _lootTable)
        {
            int key = entry.Key;
            double value = entry.Value;

            if (random <  value)
            {
                GameObject pickableDropItem = Instantiate(_pickableDropPrefab, transform.position, transform.rotation, dropContainer.transform);
                GameObject pickableItem = Instantiate(_pickablePrefab, pickableContainer.transform);
                pickableItem.GetComponent<PickableItem>()?.SetItem(key);
                pickableItem.GetComponent<FollowCanvas>()?.SetLookAt(pickableDropItem);
            }
        }
        base.Die();

    }
}
