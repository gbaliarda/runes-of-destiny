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
    public void EventOpenMenu(bool open)
    {
        if (OnOpenMenu != null) OnOpenMenu(open);
    }

    public void EventOpenInventory(bool open)
    {
        if (OnOpenInventory != null) OnOpenInventory(open);
    }

    public void EventItemChanged()
    {
        if (OnItemChanged != null) OnItemChanged();
    }
    #endregion
}
