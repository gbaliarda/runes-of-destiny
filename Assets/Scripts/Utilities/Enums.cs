using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    #region SINGLETON
    static public Enums instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    public enum Levels
    {
        MainMenu,
        LoadScreen,
        Level1
    }

    public enum ItemType
    {
        Armor,
        Weapon,
        Helmet,
        Belt,
        Boots,
        Bracers,
        Gloves,
        Necklaces,
        Rings,
        Pants,
        Shoulders
    }
}
