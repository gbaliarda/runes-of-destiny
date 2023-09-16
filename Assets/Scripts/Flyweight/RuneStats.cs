using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuneStats", menuName = "Stats/Rune", order = 0)]
public class RuneStats : ScriptableObject
{
    [SerializeField] private RuneStatsValues _runeStats;

    public int Damage => _runeStats.Damage;

    public int ManaCost => _runeStats.ManaCost;

    public float Cooldown => _runeStats.Cooldown;

}

[System.Serializable]
public struct RuneStatsValues
{
    public int Damage;
    public int ManaCost;
    public float Cooldown;
}
