using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuneStats", menuName = "Stats/Rune", order = 0)]
public class RuneStats : ScriptableObject
{
    [SerializeField] private RuneStatsValues _runeStats;
    [SerializeField] private DamageStatsValues _damageStats;

    public int PhysicalDamage => _damageStats.PhysicalDamage;
    public int FireDamage => _damageStats.FireDamage;
    public int WaterDamage => _damageStats.WaterDamage;
    public int LightningDamage => _damageStats.LightningDamage;
    public int VoidDamage => _damageStats.VoidDamage;
    public int TotalDamage => PhysicalDamage + FireDamage + WaterDamage + LightningDamage + VoidDamage;

    public DamageStatsValues Damage => _damageStats;

    public int ManaCost => _runeStats.ManaCost;

    public float Cooldown => _runeStats.Cooldown;

}

[System.Serializable]
public struct RuneStatsValues
{
    public int ManaCost;
    public float Cooldown;
}
