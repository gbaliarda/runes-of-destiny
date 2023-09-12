using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Stats/Entities", order = 0)]
public class EntityStats : ScriptableObject
{
    [SerializeField] private EntityStatsValues _stats;

    public int MaxLife => _stats.MaxLife;

}

[System.Serializable]
public struct EntityStatsValues
{
    public int MaxLife;

}