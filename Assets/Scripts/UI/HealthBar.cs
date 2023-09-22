using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Player _player;
    private Slider _slider;
    private int _maxHealth;

    void Start()
    {
        _maxHealth = _player.Stats.MaxLife;
        _slider = GetComponent<Slider>();
        UpdateHealth(_maxHealth);
        EventsManager.instance.OnPlayerTakeDamage += OnPlayerTakeDamage;
        EventsManager.instance.OnHealthPotUse += OnHealthPotUse;
    }

    private void UpdateHealth(int hp)
    {
        _slider.value = hp / (float)_maxHealth;
        _text.text = $"{hp}/{_maxHealth}";
    }

    private void OnPlayerTakeDamage(int hp)
    {
        if (hp < 0) hp = 0;
        UpdateHealth(hp);
    }

    private void OnHealthPotUse(int currentHp, float cooldown)
    {
        UpdateHealth(currentHp);
    }
}
