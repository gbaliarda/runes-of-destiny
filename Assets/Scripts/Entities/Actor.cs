using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class Actor : MonoBehaviour, IDamageable
{
    #region PRIVATE_PROPERTIES
    [SerializeField] protected EntityStats stats;
    public EntityStats Stats => stats;
    protected int life;
    protected HealthPotionController healthPotionController;
    protected bool isDead = false;
    protected bool isGameOver = false;
    protected AudioSource audioSource;

    public AudioSource AudioSource => audioSource;
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
        if (isDead) return 0;
        life -= damage.PhysicalDamage + damage.FireDamage + damage.WaterDamage + damage.LightningDamage + damage.VoidDamage;
        if (life <= 0) Die();
        return life;
    }

    public virtual int HealDamage(int damage)
    {
        if (isDead) return 0;
        int healthToFull = stats.MaxLife - life;
        int maximumHealthRecovered = damage;
        life += healthToFull < maximumHealthRecovered ? healthToFull : maximumHealthRecovered;
        return life;
    }

    public void OnGameOver(bool isVictory)
    {
        isGameOver = true;
    }

    #endregion

    #region UNITY_EVENTS
    protected void Start()
    {
        life = MaxLife;
        healthPotionController = GetComponent<HealthPotionController>();
        EventsManager.instance.OnGameOver += OnGameOver;
        audioSource = GetComponent<AudioSource>();
    }
    #endregion
}
