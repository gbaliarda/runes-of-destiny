using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedRuneStats", menuName = "Stats/RangedRune", order = 0)]
public class RangedRuneStats : RuneStats
{
    [SerializeField] private RangedRuneStatsValues _rangedRuneStats;

    public float Speed => _rangedRuneStats.Speed;

    public int Projectiles => _rangedRuneStats.Projectiles;

}

[System.Serializable]
public struct RangedRuneStatsValues
{
    public float Speed;
    public int Projectiles;
}
