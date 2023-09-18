using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private Slider _slider;
    private float maxHealth = 100f;

    void Start()
    {
        _slider = GetComponent<Slider>();
        EventsManager.instance.OnPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnPlayerTakeDamage(int hp)
    {
        if (hp < 0) hp = 0;
        _slider.value = hp/maxHealth;
        _text.text = $"{hp}/{maxHealth}";
    }
}
