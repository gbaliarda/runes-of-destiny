using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuneStats", menuName = "Stats/Rune", order = 0)]
public class RuneStats : ScriptableObject
{
    [SerializeField] private RuneStatsValues _runeStats;

    public float Speed => _runeStats.Speed;

    public int Damage => _runeStats.Damage;

    public int ManaCost => _runeStats.ManaCost;

    public float Cooldown => _runeStats.Cooldown;

    public int Projectiles => _runeStats.Projectiles;

}

[System.Serializable]
public struct RuneStatsValues
{
    public float Speed;
    public int Damage;
    public int ManaCost;
    public float Cooldown;
    public int Projectiles;
}
