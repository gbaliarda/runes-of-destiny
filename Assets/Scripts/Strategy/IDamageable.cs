using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxLife { get; }
    int Life { get; }
    bool IsDead { get; }
    int TakeDamage(DamageStatsValues damage);
    void Die();
    int HealDamage(int damage);
}
