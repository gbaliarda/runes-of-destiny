using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rune : MonoBehaviour, IRune
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private GameObject _rangedRunePrefab;
    [SerializeField] private Transform _rangedRuneContainer;
    [SerializeField] private RuneStats _runeStats;
    [SerializeField] private Character _player;
    protected float _cooldownLeft;
    #endregion

    #region IRANGEDRUNE_PROPERTIES
    public GameObject RunePrefab => _rangedRunePrefab;
    public Transform RuneContainer => _rangedRuneContainer;
    public RuneStats RuneStats => _runeStats;
    public Character Player => _player;
    #endregion

    #region IRANGEDRUNE_METHODS

    public virtual void Shoot() => Debug.Log("Shooting");

    public virtual void ShootAtDirection(Vector3 direction) => Debug.Log("Shooting at direection");
    #endregion

    #region UNITY_EVENTS
    void Start()
    {
        _cooldownLeft = 0;
    }

    void Update()
    {
        if (_cooldownLeft > 0) _cooldownLeft -= Time.deltaTime;
    }
    #endregion
}
