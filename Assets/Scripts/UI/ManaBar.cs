using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Player _player;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        UpdateMana(_player.CharacterStats.MaxMana);
        EventsManager.instance.OnPlayerSpendMana += OnPlayerSpendMana;
    }

    private void UpdateMana(int mana)
    {
        int maxMana = _player.CharacterStats.MaxMana;
        _slider.value = mana / (float)maxMana;
        _text.text = $"{mana}/{maxMana}";
    }

    private void OnPlayerSpendMana(int mana)
    {
        UpdateMana(mana);
    }
}
