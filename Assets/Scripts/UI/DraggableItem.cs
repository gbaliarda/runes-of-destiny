using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Enums;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int _itemId;
    private CharacterStats _itemStats;
    private ItemType _itemType;
    private Sprite _sprite;
    [SerializeField] private bool _equipped;
    private string _name;

    private Database _database;
    
    private Image _image;
    private Transform _parentAfterDrag;

    public string Name => _name;
    public int ItemId => _itemId;
    public bool IsEquipped => _equipped;
    public CharacterStats ItemStats => _itemStats;
    public ItemType ItemType => _itemType;
    public Transform ParentAfterDrag => _parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_itemId == 0) return;
        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_itemId == 0) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_itemId == 0) return;
        transform.SetParent(_parentAfterDrag);
        _image.raycastTarget = true;
    }

    public void SetParentAfterDrag(Transform parentAfterDrag)
    {
        _parentAfterDrag = parentAfterDrag;
    }

    public void Awake()
    {
        _image = GetComponent<Image>();

        _database = new Database();
    }

    public void Start()
    {
        if (_sprite == null && _itemId > 0)
        {
            UpdateItem(_itemId);
        }

        if (_sprite != null)
        {
            Debug.Log(_sprite);
            Debug.Log(_itemStats);
            _image.color = Color.white;
            _image.sprite = _sprite;
        }
    }

    public void UpdateItem(int itemId)
    {
        if (_database == null) _database = new Database();
        ItemData itemData = _database.GetItem(itemId);
        if (itemData != null && itemData.Sprite != null)
        {
            Debug.Log($"Item found with id {itemId}");
            _itemId = itemId;
            _itemStats = itemData.ItemStats;
            _sprite = itemData.Sprite;
            _itemType = itemData.ItemType;
            _name = itemData.Name;

            if (_image == null) _image = GetComponent<Image>();

            _image.color = Color.white;
            _image.sprite = _sprite;

            _equipped = false;
        }
    }

    public void RemoveItem()
    {
        _itemId = 0;
        _sprite = null;
        _image.color = Color.black;
        _image.sprite = null;
        _equipped = false;
        _name = "";
        _itemStats = null;
        _itemType = 0;
    }

    public void EquipItem()
    {
        if (_sprite == null) return;
        EventsManager.instance.EventEquipItem(this);
        if (_equipped)
        {
            _image.color = Color.white;
            _equipped = false;
        }
        else
        {
            _image.color = Color.green;
            _equipped = true;
        }
    }

    public void SetEquipped(bool equipped)
    {
        _image.color = equipped ? Color.green : Color.white;
        _equipped = equipped;
    }

    public void UnequipItem()
    {
        SetEquipped(false);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        EquipItem();
    }
}
