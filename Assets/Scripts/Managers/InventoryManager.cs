using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    static public InventoryManager instance;

    #region UNITY_EVENTS
    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Start()
    {
        EventsManager.instance.OnOpenInventory += OnOpenInventory;
        EventsManager.instance.OnItemChanged += UpdateUI;
    }
    #endregion

    void UpdateUI()
    {
        Debug.Log("asd");
    }

    void OnOpenInventory(bool isOpen)
    {

    }
}
