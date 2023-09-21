using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/Characters", order = 0)]
public class CharacterStats : EntityStats
{
    [SerializeField] private CharacterStatsValues _characterStats;

    public float MovementSpeed => Mathf.RoundToInt(_characterStats.MovementSpeed + _characterStats.Dexterity * 0.01f);
    public int MaxMana => Mathf.RoundToInt(_characterStats.MaxMana + _characterStats.Intelligence * 0.01f);
    public int ManaRegen => Mathf.RoundToInt(_characterStats.ManaRegen + _characterStats.Intelligence * 0.05f);
    public int Dexterity => _characterStats.Dexterity;
    public int Intelligence => _characterStats.Intelligence;
    public int Strength => _characterStats.Strength;

}

[System.Serializable]
public struct CharacterStatsValues
{
    public float MovementSpeed;
    public int ManaRegen;
    public int MaxMana;
    public int Dexterity;
    public int Intelligence;
    public int Strength;

}
