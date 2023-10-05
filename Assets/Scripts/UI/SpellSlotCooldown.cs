using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlotCooldown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _runeIndex;
    private Slider _slider;
    private float _maxCooldown;
    private float _cooldownLeft;

    void Start()
    {
        _slider = GetComponent<Slider>();
        EventsManager.instance.OnAbilityUse += OnAbilityUse;
    }

    private void OnAbilityUse(int runeIndex, float cooldown)
    {
        if (runeIndex != _runeIndex) return;
        _maxCooldown = cooldown;
        _cooldownLeft = cooldown;
    }

    private void Update()
    {
        if(_cooldownLeft >= 0) UpdateUI();
    }

    private void UpdateUI()
    {
        _cooldownLeft -= Time.deltaTime;
        _slider.value = _cooldownLeft / _maxCooldown;
        _text.text = $"{_cooldownLeft:F1}";
        if (_cooldownLeft <= 0) _text.text = "";
    }
}
