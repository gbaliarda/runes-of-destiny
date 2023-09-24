using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable
{
    #region PRIVATE_PROPERTIES
    [SerializeField] protected EntityStats stats;
    public EntityStats Stats => stats;
    protected int life;
    protected HealthPotionController healthPotionController;
    [SerializeField] protected bool isDead = false;
    #endregion

    #region IDAMAGEABLE_PROPERTIES
    public int MaxLife => stats.MaxLife;

    public int Life => life;

    public bool IsDead => isDead;
    #endregion

    #region IDAMAGEABLE_METHODS
    public virtual void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public virtual int TakeDamage(DamageStatsValues damage)
    {
        life -= damage.PhysicalDamage + damage.FireDamage + damage.WaterDamage + damage.LightningDamage + damage.VoidDamage;
        if (life <= 0) Die();
        return life;
    }

    public virtual int HealDamage(int damage)
    {
        int healthToFull = stats.MaxLife - life;
        int maximumHealthRecovered = damage;
        life += healthToFull < maximumHealthRecovered ? healthToFull : maximumHealthRecovered;
        return life;
    }

    #endregion

    #region UNITY_EVENTS
    protected void Start()
    {
        life = MaxLife;
        healthPotionController = GetComponent<HealthPotionController>();
    }
    #endregion
}
