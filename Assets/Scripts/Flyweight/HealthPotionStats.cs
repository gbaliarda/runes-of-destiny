using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotionStats", menuName = "Stats/HealthPotion", order = 0)]
public class HealthPotionStats : ScriptableObject
{
    [SerializeField] private HealthPotionStatsValues _stats;

    public int HealAmount => _stats.HealAmount;
    public int HealthPotCharges => _stats.HealthPotCharges;
    public float HealthPotCooldown => _stats.HealthPotCooldown;
    public float HealthPotChargeRegenerationRate => _stats.HealthPotChargeRegenerationRate;
}

[System.Serializable]
public struct HealthPotionStatsValues
{
    public int HealAmount;
    public int HealthPotCharges;
    public float HealthPotCooldown;
    public float HealthPotChargeRegenerationRate;
}