using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/Characters", order = 0)]
public class CharacterStats : EntityStats
{
    [SerializeField] private CharacterStatsValues _characterStats;
    [SerializeField] private CharacterDefensiveStatsValues _characterDefensiveStats;
    [SerializeField] private CharacterOffensiveStatsValues _characterOffensiveStats;

    public int MovementSpeed => Mathf.RoundToInt(_characterStats.MovementSpeed + _characterStats.Dexterity * 0.01f);
    
    public int Level => _characterStats.Level; 
    public float Experience => _characterStats.Experience;

    public int MaxMana => Mathf.RoundToInt(_characterStats.MaxMana + _characterStats.Intelligence * 0.01f);
    public int ManaRegen => Mathf.RoundToInt(_characterStats.ManaRegen + _characterStats.Intelligence * 0.05f);
    
    public int MagicShield => _characterStats.MagicShield;
    public int MagicShieldRegen => _characterStats.MagicShieldRegen;
    public int HealthRegen => _characterStats.HealthRegen;
    public int LifeLeech => _characterStats.LifeLeech;
    public int ManaLeech => _characterStats.ManaLeech;
    
    public int Dexterity => _characterStats.Dexterity;
    public int Intelligence => _characterStats.Intelligence;
    public int Strength => _characterStats.Strength;


    public int Armor => _characterDefensiveStats.Armor;
    public int EvasionChance => _characterDefensiveStats.EvasionChance;
    public int BlockSpellChance => _characterDefensiveStats.BlockSpellChance;
    public int DamageBlockedAmount => _characterDefensiveStats.DamageBlockedAmount;
    public int WaterResistance => _characterDefensiveStats.WaterResistance;
    public int FireResistance => _characterDefensiveStats.FireResistance;
    public int LightningResistance => _characterDefensiveStats.LightningResistance;
    public int VoidResistance => _characterDefensiveStats.VoidResistance;
    public int StunResistance => _characterDefensiveStats.StunResistance;
    public int CurseResistance => _characterDefensiveStats.CurseResistance;
    public int PoisonResistance => _characterDefensiveStats.PoisonResistance;
    public int Stealth => _characterDefensiveStats.Stealth;

    public int SpellPower => _characterOffensiveStats.SpellPower;
    public int AttackPower => _characterOffensiveStats.AttackPower;
    public float AttackSpeed => _characterOffensiveStats.AttackSpeed;
    public float CastSpeed => _characterOffensiveStats.CastSpeed;
    public int Luck => _characterOffensiveStats.Luck;
    public float CooldownReduction => _characterOffensiveStats.CooldownReduction;
    public float CriticalChance => _characterOffensiveStats.CriticalChance;
    public float CriticalDamage => _characterOffensiveStats.CriticalDamage;
    public float Accuracy => _characterOffensiveStats.Accuracy;
    public int WaterPenetration => _characterOffensiveStats.WaterPenetration;
    public int FirePenetration => _characterOffensiveStats.FirePenetration;
    public int LightningPenetration => _characterOffensiveStats.LightningPenetration;
    public int VoidPenetration => _characterOffensiveStats.VoidPenetration;
    public int ArmorPenetration => _characterOffensiveStats.ArmorPenetration;
    public int ReflectDamage => _characterOffensiveStats.ReflectDamage;

    public void SetEntityStats(EntityStatsValues values)
    {
        stats = values;
    }

    public void SetCharacterStatsValues(CharacterStatsValues values)
    {
        _characterStats = values;
    }

    public void BuffMaxLife(int maxLifeBuff)
    {
        stats.MaxLife += maxLifeBuff;
    }

    public void AddStats(CharacterStats otherStats)
    {
        stats.MaxLife += otherStats.MaxLife;

        _characterStats.MovementSpeed += otherStats.MovementSpeed;

        _characterStats.MaxMana += otherStats.MaxMana;
        _characterStats.ManaRegen += otherStats.ManaRegen;
        _characterStats.MagicShield += otherStats.MagicShield;
        _characterStats.MagicShieldRegen += otherStats.MagicShieldRegen;
        _characterStats.HealthRegen += otherStats.HealthRegen;
        _characterStats.LifeLeech += otherStats.LifeLeech;
        _characterStats.ManaLeech += otherStats.ManaLeech;

        _characterStats.Dexterity += otherStats.Dexterity;
        _characterStats.Intelligence += otherStats.Intelligence;
        _characterStats.Strength += otherStats.Strength;

        _characterDefensiveStats.Armor += otherStats.Armor;
        _characterDefensiveStats.EvasionChance += otherStats.EvasionChance;
        _characterDefensiveStats.BlockSpellChance += otherStats.BlockSpellChance;
        _characterDefensiveStats.DamageBlockedAmount += otherStats.DamageBlockedAmount;
        _characterDefensiveStats.WaterResistance += otherStats.WaterResistance;
        _characterDefensiveStats.FireResistance += otherStats.FireResistance;
        _characterDefensiveStats.LightningResistance += otherStats.LightningResistance;
        _characterDefensiveStats.VoidResistance += otherStats.VoidResistance;
        _characterDefensiveStats.StunResistance += otherStats.StunResistance;
        _characterDefensiveStats.CurseResistance += otherStats.CurseResistance;
        _characterDefensiveStats.PoisonResistance += otherStats.PoisonResistance;
        _characterDefensiveStats.Stealth += otherStats.Stealth;

        _characterOffensiveStats.SpellPower += otherStats.SpellPower;
        _characterOffensiveStats.AttackPower += otherStats.AttackPower;
        _characterOffensiveStats.AttackSpeed += otherStats.AttackSpeed;
        _characterOffensiveStats.CastSpeed += otherStats.CastSpeed;
        _characterOffensiveStats.Luck += otherStats.Luck;
        _characterOffensiveStats.CooldownReduction += otherStats.CooldownReduction;
        _characterOffensiveStats.CriticalChance += otherStats.CriticalChance;
        _characterOffensiveStats.CriticalDamage += otherStats.CriticalDamage;
        _characterOffensiveStats.Accuracy += otherStats.Accuracy;
        _characterOffensiveStats.WaterPenetration += otherStats.WaterPenetration;
        _characterOffensiveStats.FirePenetration += otherStats.FirePenetration;
        _characterOffensiveStats.LightningPenetration += otherStats.LightningPenetration;
        _characterOffensiveStats.VoidPenetration += otherStats.VoidPenetration;
        _characterOffensiveStats.ArmorPenetration += otherStats.ArmorPenetration;
        _characterOffensiveStats.ReflectDamage += otherStats.ReflectDamage;
    }


    public void RemoveStats(CharacterStats otherStats)
    {
        stats.MaxLife -= otherStats.MaxLife;

        _characterStats.MovementSpeed -= otherStats.MovementSpeed;

        _characterStats.MaxMana -= otherStats.MaxMana;
        _characterStats.ManaRegen -= otherStats.ManaRegen;
        _characterStats.MagicShield -= otherStats.MagicShield;
        _characterStats.MagicShieldRegen -= otherStats.MagicShieldRegen;
        _characterStats.HealthRegen -= otherStats.HealthRegen;
        _characterStats.LifeLeech -= otherStats.LifeLeech;
        _characterStats.ManaLeech -= otherStats.ManaLeech;

        _characterStats.Dexterity -= otherStats.Dexterity;
        _characterStats.Intelligence -= otherStats.Intelligence;
        _characterStats.Strength -= otherStats.Strength;

        _characterDefensiveStats.Armor -= otherStats.Armor;
        _characterDefensiveStats.EvasionChance -= otherStats.EvasionChance;
        _characterDefensiveStats.BlockSpellChance -= otherStats.BlockSpellChance;
        _characterDefensiveStats.DamageBlockedAmount -= otherStats.DamageBlockedAmount;
        _characterDefensiveStats.WaterResistance -= otherStats.WaterResistance;
        _characterDefensiveStats.FireResistance -= otherStats.FireResistance;
        _characterDefensiveStats.LightningResistance -= otherStats.LightningResistance;
        _characterDefensiveStats.VoidResistance -= otherStats.VoidResistance;
        _characterDefensiveStats.StunResistance -= otherStats.StunResistance;
        _characterDefensiveStats.CurseResistance -= otherStats.CurseResistance;
        _characterDefensiveStats.PoisonResistance -= otherStats.PoisonResistance;
        _characterDefensiveStats.Stealth -= otherStats.Stealth;

        _characterOffensiveStats.SpellPower -= otherStats.SpellPower;
        _characterOffensiveStats.AttackPower -= otherStats.AttackPower;
        _characterOffensiveStats.AttackSpeed -= otherStats.AttackSpeed;
        _characterOffensiveStats.CastSpeed -= otherStats.CastSpeed;
        _characterOffensiveStats.Luck -= otherStats.Luck;
        _characterOffensiveStats.CooldownReduction -= otherStats.CooldownReduction;
        _characterOffensiveStats.CriticalChance -= otherStats.CriticalChance;
        _characterOffensiveStats.CriticalDamage -= otherStats.CriticalDamage;
        _characterOffensiveStats.Accuracy -= otherStats.Accuracy;
        _characterOffensiveStats.WaterPenetration -= otherStats.WaterPenetration;
        _characterOffensiveStats.FirePenetration -= otherStats.FirePenetration;
        _characterOffensiveStats.LightningPenetration -= otherStats.LightningPenetration;
        _characterOffensiveStats.VoidPenetration -= otherStats.VoidPenetration;
        _characterOffensiveStats.ArmorPenetration -= otherStats.ArmorPenetration;
        _characterOffensiveStats.ReflectDamage -= otherStats.ReflectDamage;
    }
}

[System.Serializable]
public struct CharacterStatsValues
{
    public int MovementSpeed;
    public int Level;
    public float Experience;
    public int ManaRegen;
    public int MaxMana;
    public int MagicShield;
    public int MagicShieldRegen;
    public int HealthRegen;
    public int LifeLeech;
    public int ManaLeech;
    
    public int Dexterity;
    public int Intelligence;
    public int Strength;
}

[System.Serializable]
public struct CharacterDefensiveStatsValues
{
    public int Armor;
    public int EvasionChance;
    public int BlockSpellChance;
    public int DamageBlockedAmount;
    public int WaterResistance;
    public int FireResistance;
    public int LightningResistance;
    public int VoidResistance;
    public int StunResistance;
    public int CurseResistance;
    public int PoisonResistance;
    public int ManaEfficiency;
    public int Stealth;
}

[System.Serializable]
public struct CharacterOffensiveStatsValues
{
    public int SpellPower;
    public int AttackPower;
    public float AttackSpeed;
    public float CastSpeed;
    public int Luck;
    public float CooldownReduction;
    public float CriticalChance;
    public float CriticalDamage;
    public float Accuracy;
    public int WaterPenetration;
    public int FirePenetration;
    public int LightningPenetration;
    public int VoidPenetration;
    public int ElementalAffinity;
    public int ArmorPenetration;
    public int ReflectDamage;
}
