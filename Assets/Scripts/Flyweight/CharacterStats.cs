using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/Characters", order = 0)]
public class CharacterStats : EntityStats
{
    [SerializeField] private CharacterStatsValues _characterStats;
    private float _dexterityIncreaseMovementSpeed = 0.01f;
    private float _intelligenceIncreaseMaxMana = 0.01f;
    private float _intelligenceIncreaseManaRegen = 0.05f;

    public float MovementSpeed => Mathf.RoundToInt(_characterStats.MovementSpeed + _characterStats.Dexterity * _dexterityIncreaseMovementSpeed);
    public int MaxMana => Mathf.RoundToInt(_characterStats.MaxMana + _characterStats.Intelligence * _intelligenceIncreaseMaxMana);
    public int ManaRegen => Mathf.RoundToInt(_characterStats.ManaRegen + _characterStats.Intelligence * _intelligenceIncreaseManaRegen);
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
