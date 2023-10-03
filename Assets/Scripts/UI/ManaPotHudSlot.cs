using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaPotHudSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _icon;
    [SerializeField] private Slider _slider;
    [SerializeField] private List<Sprite> _potionSprites = new List<Sprite>();
    private float _maxCooldown;
    private float _cooldownLeft;

    void Start()
    {
        EventsManager.instance.OnManaPotUse += OnManaPotUse;
        EventsManager.instance.OnUpdateManaPotCharge += OnUpdateManaPotCharge;
    }

    private void OnManaPotUse(float cooldown)
    {
        _maxCooldown = cooldown;
        _cooldownLeft = cooldown;
    }

    private void OnUpdateManaPotCharge(int currentCharges)
    {
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
