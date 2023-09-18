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
    #endregion
}
