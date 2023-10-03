using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotHudSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _icon;
    [SerializeField] private Slider _slider;
    [SerializeField] private List<Sprite> _potionSprites = new List<Sprite>();
    private float _maxCooldown;
    private float _cooldownLeft;

    void Start()
    {
        EventsManager.instance.OnHealthPotUse += OnHealthPotUse;
        EventsManager.instance.OnUpdateHpPotCharge += OnUpdateHpPotCharge;
    }

    private void OnHealthPotUse(float cooldown)
    {
        _maxCooldown = cooldown;
        _cooldownLeft = cooldown;
    }

    private void OnUpdateHpPotCharge(int currentCharges)
    {
        Debug.Log($"Updating HP Pot charge with {currentCharges}");
        if (_potionSprites.Count <= currentCharges) return;
        _icon.sprite = _potionSprites[currentCharges];
    }

    private void Update()
    {
        if(_cooldownLeft >= 0) UpdateUI();
    }

    private void UpdateUI()
    {
        _cooldownLeft -= Time.deltaTime;
        _slider.value = _cooldownLeft / _maxCooldown;
        _text.text = $"{_cooldownLeft:F2}";
        if (_cooldownLeft <= 0) _text.text = "";
    }
}
