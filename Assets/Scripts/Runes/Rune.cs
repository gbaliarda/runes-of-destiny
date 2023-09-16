using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rune : MonoBehaviour, IRune
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private GameObject _runePrefab;
    [SerializeField] private Transform _runeContainer;
    [SerializeField] protected RuneStats runeStats;
    [SerializeField] private Character _player;
    protected float _cooldownLeft;
    #endregion

    #region IRUNE_PROPERTIES
    public GameObject RunePrefab => _runePrefab;
    public Transform RuneContainer => _runeContainer;
    public RuneStats RuneStats => runeStats;
    public Character Player => _player;
    #endregion

    #region IRUNE_METHODS

    public virtual void Shoot() => Debug.Log("Shooting");

    public virtual void ShootAtDirection(Vector3 direction) => Debug.Log("Shooting at direection");
    #endregion

    #region UNITY_EVENTS
    protected void Start()
    {
        _cooldownLeft = 0;
    }

    protected void Update()
    {
        if (_cooldownLeft > 0) _cooldownLeft -= Time.deltaTime;
    }
    #endregion
}
