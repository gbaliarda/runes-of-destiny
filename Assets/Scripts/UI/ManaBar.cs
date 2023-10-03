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
    private int _maxMana;

    private void Start()
    {
        _maxMana = _player.CharacterStats.MaxMana;
        _slider = GetComponent<Slider>();
        UpdateMana(_maxMana);
        EventsManager.instance.OnPlayerSpendMana += OnPlayerSpendMana;
    }

    private void UpdateMana(int mana)
    {
        _slider.value = mana / (float)_maxMana;
        _text.text = $"{mana}/{_maxMana}";
    }

    private void OnPlayerSpendMana(int mana)
    {
        UpdateMana(mana);
    }
}
