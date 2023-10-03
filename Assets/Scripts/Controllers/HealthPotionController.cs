using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Actor))]
public class HealthPotionController : MonoBehaviour, IHealthPotion
{
    [SerializeField] private HealthPotionStats _healthPotionStats;
    [SerializeField] private UsePotionSound _potionSound;
    private Actor _actor;
    private int _hpPotChargesLeft;
    private float _currentHpPotCooldown;
    private float _currentChargeRegenerationCycle;
    public int HpPotChargesLeft => _hpPotChargesLeft;
    public float CurrentHpPotCooldowm => _currentHpPotCooldown;
    public float CurrentChargeRegenerationCycle => _currentChargeRegenerationCycle;

    public void Heal()
    {
        if (_currentHpPotCooldown > 0) return;
        if (_hpPotChargesLeft == 0) return;
        EventQueueManager.instance.AddCommand(new CmdHealDamage(_actor, _healthPotionStats.HealAmount));
        _currentHpPotCooldown = _healthPotionStats.HealthPotCooldown;
        _currentChargeRegenerationCycle = _healthPotionStats.HealthPotChargeRegenerationRate;
        _hpPotChargesLeft -= 1;
        if (_actor is Player)
        {
            if (_potionSound != null) EventQueueManager.instance.AddCommand(new CmdPlaySound(_potionSound));
            EventsManager.instance.EventHealthPotUse(_currentHpPotCooldown);
            EventsManager.instance.EventUpdateHpPotCharge(_hpPotChargesLeft);
        }
    }

    private void RegenerateHpPotCharge()
    {
        if (_currentChargeRegenerationCycle > 0) return;
        _currentChargeRegenerationCycle = _healthPotionStats.HealthPotChargeRegenerationRate;
        _hpPotChargesLeft += 1;
        if (_actor is Player) EventsManager.instance.EventUpdateHpPotCharge(_hpPotChargesLeft);
    }
    void Start()
    {
        _hpPotChargesLeft = _healthPotionStats.HealthPotCharges;
        _currentHpPotCooldown = 0;
        _currentChargeRegenerationCycle = _healthPotionStats.HealthPotChargeRegenerationRate;
        _actor = GetComponent<Actor>();
    }

    void Update()
    {
        if (_currentHpPotCooldown > 0) _currentHpPotCooldown -= Time.deltaTime;
        if (_hpPotChargesLeft < _healthPotionStats.HealthPotCharges) _currentChargeRegenerationCycle -= Time.deltaTime;
        RegenerateHpPotCharge();
    }
}
