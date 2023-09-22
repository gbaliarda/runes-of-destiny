using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaPotionStats", menuName = "Stats/ManaPotion", order = 0)]
public class ManaPotionStats : ScriptableObject
{
    [SerializeField] private ManaPotionStatsValues _stats;

    public int ManaAmount => _stats.ManaAmount;
    public int ManaPotCharges => _stats.ManaPotCharges;
    public float ManaPotCooldown => _stats.ManaPotCooldown;
    public float ManaPotChargeRegenerationRate => _stats.ManaPotChargeRegenerationRate;
}

[System.Serializable]
public struct ManaPotionStatsValues
{
    public int ManaAmount;
    public int ManaPotCharges;
    public float ManaPotCooldown;
    public float ManaPotChargeRegenerationRate;
}