using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeRuneStats", menuName = "Stats/MeleeRune", order = 0)]
public class MeleeRuneStats : RuneStats
{
    [SerializeField] private MeleeRuneStatsValues _meleeRuneStats;
}

[System.Serializable]
public struct MeleeRuneStatsValues
{
}
