using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private Slider _slider;
    private float _maxMana = 100f;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        EventsManager.instance.OnPlayerSpendMana += OnPlayerSpendMana;
    }

    private void OnPlayerSpendMana(int mana)
    {
        _slider.value = mana / _maxMana;
        _text.text = $"{mana}/{_maxMana}";
    }
}
