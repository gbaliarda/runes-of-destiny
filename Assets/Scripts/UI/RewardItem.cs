using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Enums;

public class RewardItem : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int _itemId;
    private CharacterStats _itemStats;
    private Sprite _sprite;
    private string _name;

    private Database _database;
    
    private Image _image;

    public string Name => _name;

    public CharacterStats ItemStats => _itemStats;
    public int ItemId => _itemId;

    public void Awake()
    {
        _image = GetComponent<Image>();

        _database = new Database();
    }

    public void Start()
    {

    }

    public void UpdateItem(int itemId)
    {
        if (_database == null) _database = new Database();
        ItemData itemData = _database.GetItem(itemId);
        if (itemData != null && itemData.Sprite != null)
        {
            _itemId = itemId;
            _itemStats = itemData.ItemStats;
            _sprite = itemData.Sprite;
            _name = itemData.Name;

            if (_image == null) _image = GetComponent<Image>();

            _image.color = Color.white;
            _image.sprite = _sprite;
        }
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
