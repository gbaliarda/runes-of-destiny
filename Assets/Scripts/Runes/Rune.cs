using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Rune : MonoBehaviour, IRune
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private GameObject _runePrefab;
    [SerializeField] private Transform _runeContainer;
    [SerializeField] protected RuneStats runeStats;
    [SerializeField] protected Character player;
    [SerializeField] private Sprite _iconSpell;
    protected float cooldownLeft;
    public float CooldownLeft => cooldownLeft;
    #endregion

    #region IRUNE_PROPERTIES
    public GameObject RunePrefab => _runePrefab;
    public Transform RuneContainer => _runeContainer;
    public RuneStats RuneStats => runeStats;
    public Character Player => player;
    #endregion

    #region IRUNE_METHODS

    public virtual void Shoot() => Debug.Log("Shooting");

    public virtual void ShootAtDirection(Vector3 direction) => Debug.Log("Shooting at direection");

    public virtual void SetCooldown(float cooldown) => cooldownLeft = cooldown;
    #endregion

    #region UNITY_EVENTS
    protected void Start()
    {
        cooldownLeft = 0;
    }

    protected void Update()
    {
        if (cooldownLeft > 0) cooldownLeft -= Time.deltaTime;
    }
    #endregion
}
