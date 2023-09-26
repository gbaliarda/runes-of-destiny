using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private Slider _slider;
    private int _targetInstanceId = 0;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void SubscribeToTarget(int targetInstanceId, string name)
    {
        _targetInstanceId = targetInstanceId;
        _textMeshPro.text = name;
        EventsManager.instance.OnTargetHealthUpdate += OnTargetHealthUpdate;
    }
    
    public void UnSubscribeToTarget()
    {
        _targetInstanceId = 0;
        EventsManager.instance.OnTargetHealthUpdate -= OnTargetHealthUpdate;
    }

    private void OnTargetHealthUpdate(int _instanceId, int life, int _maxHealth)
    {
        if (_instanceId != _targetInstanceId) return;
        _slider.value = life / (float)_maxHealth;
    }
}