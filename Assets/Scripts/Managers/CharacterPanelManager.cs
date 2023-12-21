using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPanelManager : MonoBehaviour
{
    #region SINGLETON
    static public CharacterPanelManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    [SerializeField] private TextMeshProUGUI _mainStatsValues;
    [SerializeField] private TextMeshProUGUI _defenceStatsValues;

    void Start()
    {
        gameObject.SetActive(false);
        EventsManager.instance.OnCharacterPanelOpen += OnCharacterPanelOpen;
        EventsManager.instance.OnUpdateCharacterStats += OnUpdateCharacterStats;
    }

    public void OnCharacterPanelOpen()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            UpdateMainStats();
            UpdateDefenceStats();
        }
    }

    public void OnUpdateCharacterStats()
    {
        UpdateMainStats();
        UpdateDefenceStats();
    }

    void UpdateMainStats()
    {
        Player player = Player.instance;
        
        _mainStatsValues.text = $"{player.CharacterStats.MaxLife}\n" +
                                $"{player.CharacterStats.MaxMana}\n" +
                                $"{player.CharacterStats.MagicShield}\n" +
                                $"{player.CharacterStats.Dexterity}\n" +
                                $"{player.CharacterStats.Strength}\n" +
                                $"{player.CharacterStats.Intelligence}";   
    }

    void UpdateDefenceStats()
    {
        Player player = Player.instance;

        _defenceStatsValues.text = $"{player.CharacterStats.FireResistance}%\n" +
                                   $"{player.CharacterStats.WaterResistance}%\n" +
                                   $"{player.CharacterStats.LightningResistance}%\n" +
                                   $"{player.CharacterStats.VoidResistance}%\n" +
                                   $"{player.CharacterStats.Armor}\n" +
                                   $"{player.CharacterStats.EvasionChance}%\n" +
                                   $"{player.CharacterStats.BlockSpellChance}%\n" +
                                   $"{player.CharacterStats.DamageBlockedAmount}\n" +
                                   $"{player.CharacterStats.MovementSpeed}";
    }
}
