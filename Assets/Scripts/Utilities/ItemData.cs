using UnityEngine;
using static Enums;

[System.Serializable]
public class ItemData
{
    private int _itemId;
    private string _name;
    private double _dropChance;
    private Rarity _rarity;
    private CharacterStats _itemStats;
    private Sprite _sprite;
    private ItemType _itemType;

    public int ItemId => _itemId;
    public string Name => _name;
    public CharacterStats ItemStats => _itemStats;
    public Sprite Sprite => _sprite;
    public ItemType ItemType => _itemType;
    public Rarity Rarity => _rarity;
    public double DropChance => _dropChance;
    public ItemData(int itemId, string name, CharacterStats itemStats, Sprite sprite, ItemType itemType, Rarity rarity, double dropChance)
    {
        _itemId = itemId;
        _name = name;
        _itemStats = itemStats;
        _sprite = sprite;
        _itemType = itemType;
        _rarity = rarity;
        _dropChance = dropChance;
    }
}
