using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class ManaPotionController : MonoBehaviour, IManaPotion
{
    [SerializeField] private ManaPotionStats _manaPotionStats;
    [SerializeField] private UsePotionSound _potionSound;
    private Character _character;
    private int _manaPotChargesLeft;
    private float _currentManaPotCooldown;
    private float _currentChargeRegenerationCycle;
    public int ManaPotChargesLeft => _manaPotChargesLeft;
    public float CurrentManaPotCooldowm => _currentManaPotCooldown;
    public float CurrentChargeRegenerationCycle => _currentChargeRegenerationCycle;

    public void GetMana()
    {
        if (_currentManaPotCooldown > 0) return;
        if (_manaPotChargesLeft == 0) return;
        _character.GetMana(_manaPotionStats.ManaAmount);
        _currentManaPotCooldown = _manaPotionStats.ManaPotCooldown;
        _currentChargeRegenerationCycle = _manaPotionStats.ManaPotChargeRegenerationRate;
        _manaPotChargesLeft -= 1;
        if (_character is Player)
        {
            if (_potionSound != null) EventQueueManager.instance.AddCommand(new CmdPlaySound(_potionSound));
            EventsManager.instance.EventManaPotUse(_currentManaPotCooldown);
            EventsManager.instance.EventUpdateManaPotCharge(_manaPotChargesLeft);
        }
    }

    private void RegenerateManaPotCharge()
    {
        if (_currentChargeRegenerationCycle > 0) return;
        _currentChargeRegenerationCycle = _manaPotionStats.ManaPotChargeRegenerationRate;
        _manaPotChargesLeft += 1;
        if (_character is Player) EventsManager.instance.EventUpdateManaPotCharge(_manaPotChargesLeft);
    }
    void Start()
    {
        _manaPotChargesLeft = _manaPotionStats.ManaPotCharges;
        _currentManaPotCooldown = 0;
        _currentChargeRegenerationCycle = _manaPotionStats.ManaPotChargeRegenerationRate;
        _character = GetComponent<Character>();
    }

    void Update()
    {
        if (_currentManaPotCooldown > 0) _currentManaPotCooldown -= Time.deltaTime;
        if (_manaPotChargesLeft < _manaPotionStats.ManaPotCharges) _currentChargeRegenerationCycle -= Time.deltaTime;
        RegenerateManaPotCharge();
    }
}
