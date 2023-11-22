using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Enums;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int _itemId;
    private CharacterStats _itemStats;
    private ItemType _itemType;
    [SerializeField] private Image _imageComponent;
    [SerializeField] private GameObject _button;
    private Sprite _sprite;
    [SerializeField] private bool _equipped;
    private string _name;

    private Image _buttonImage;
    private Database _database;


    public string Name => _name;
    public int ItemId => _itemId;
    public bool IsEquipped => _equipped;
    public CharacterStats ItemStats => _itemStats;
    public ItemType ItemType => _itemType;

    void Start()
    {
        _database = new Database();

        if (_sprite == null && _itemId > 0)
        {
            ItemData itemData = _database.GetItem(_itemId); 
            if (itemData != null)
            { 
                _itemStats = itemData.ItemStats;
                _sprite = itemData.Sprite;
            }
        }

        if (_sprite != null)
        {
            Debug.Log(_sprite);
            Debug.Log(_itemStats);
            _imageComponent.color = Color.white;
            _imageComponent.sprite = _sprite;
        }
        if (_button != null) _buttonImage = _button.GetComponent<Image>();
    }

    public void UpdateItem(int itemId)
    {
        
        ItemData itemData = _database.GetItem(itemId);
        if (itemData != null && itemData.Sprite != null)
        {
            Debug.Log($"Item found with id {itemId}");
            _itemId = itemId;
            _itemStats = itemData.ItemStats;
            _sprite = itemData.Sprite;
            _itemType = itemData.ItemType;
            _name = itemData.Name;

            _imageComponent.color = Color.white;
            _imageComponent.sprite = _sprite;

            _equipped = false;
        }
    }

    public void EquipItem()
    {
        if (_sprite == null || _button == null) return;
        EventsManager.instance.EventEquipItem(this);
        if (_equipped)
        {
            _buttonImage.color = Color.white;
            _equipped = false;
        } else
        {
            _buttonImage.color = Color.green;
            _equipped = true;
        }
    }

    public void UnequipItem()
    {
        _buttonImage.color = Color.white;
        _equipped = false;
    }

    public void OnMouseEnter()
    {
        Debug.Log($"Show Tooltip");
        ItemTooltipManager.instance.SetAndShowTooltip(this);
    }

    public void OnMouseExit()
    {
        ItemTooltipManager.instance.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_itemId == 0) return;
        Debug.Log($"Show Tooltip");
        ItemTooltipManager.instance.SetAndShowTooltip(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTooltipManager.instance.HideTooltip();
    }
}
