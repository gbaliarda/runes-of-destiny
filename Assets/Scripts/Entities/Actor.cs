using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable
{
    #region PRIVATE_PROPERTIES
    [SerializeField] protected EntityStats stats;
    public EntityStats Stats => stats;
    [SerializeField] private int _life;
    [SerializeField] protected bool isDead = false;
    #endregion

    #region IDAMAGEABLE_PROPERTIES
    public int MaxLife => stats.MaxLife;

    public int Life => _life;

    public bool IsDead => isDead;
    #endregion

    #region IDAMAGEABLE_METHODS
    public virtual void Die()
    {
        Debug.Log($"{name} died");
        isDead = true;
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _life -= damage;
        Debug.Log($"{name} Hit -> Life: {_life}");
        if (_life <= 0) Die();
    }
    #endregion

    #region UNITY_EVENTS
    protected void Start()
    {
        _life = MaxLife;
    }

    void Update()
    {
        
    }
    #endregion
}
