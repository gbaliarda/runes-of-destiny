using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    static public EventsManager instance;

    #region UNITY_EVENTS
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    #endregion

    #region GAME_MANAGER_ACTIONS
    public event Action<bool> OnGameOver;

    public void EventGameOver(bool isVictory)
    {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    #endregion
    #region UI_ACTIONS
    public event Action<bool> OnOpenMenu;
    public event Action<bool> OnOpenInventory;
    public event Action OnItemChanged;
    public event Action<int> OnPlayerTakeDamage;
    public event Action<int> OnPlayerSpendMana;
    public event Action<int, float> OnAbilityUse;
    public event Action<int, float> OnHealthPotUse;
    public event Action<int> OnUpdateHpPotCharge;
    public event Action<int, float> OnManaPotUse;
    public event Action<int> OnUpdateManaPotCharge;
    public void EventOpenMenu(bool open)
    {
        OnOpenMenu?.Invoke(open);
    }

    public void EventOpenInventory(bool open)
    {
        OnOpenInventory?.Invoke(open);
    }

    public void EventItemChanged()
    {
        OnItemChanged?.Invoke();
    }

    public void EventTakeDamage(int currentHp)
    {
        OnPlayerTakeDamage?.Invoke(currentHp);

    }
    public void EventSpendMana(int currentMana)
    {
        OnPlayerSpendMana?.Invoke(currentMana);
    }
    
    public void EventAbilityUse(int abilityUsed, float cooldown)
    {
        OnAbilityUse?.Invoke(abilityUsed, cooldown);
    }

    public void EventHealthPotUse(int currentHp, float cooldown)
    {
        OnHealthPotUse?.Invoke(currentHp, cooldown);
    }

    public void EventUpdateHpPotCharge(int currentCharges)
    {
        OnUpdateHpPotCharge?.Invoke(currentCharges);
    }
    
    public void EventManaPotUse(int currentMana, float cooldown)
    {
        OnManaPotUse?.Invoke(currentMana, cooldown);
    }

    public void EventUpdateManaPotCharge(int currentCharges)
    {
        OnUpdateManaPotCharge?.Invoke(currentCharges);
    }

    #endregion
}
