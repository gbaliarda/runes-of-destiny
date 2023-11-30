using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private DraggableItem _item;

    public DraggableItem Item => _item;
    public void OnDrop(PointerEventData eventData)
    {
        if (_item.ItemId > 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            int auxItemId = draggableItem.ItemId;
            bool itemWasEquipped = draggableItem.IsEquipped;
            bool currentItemWasEquipped = _item.IsEquipped;
            draggableItem.UpdateItem(_item.ItemId);
            draggableItem.SetEquipped(currentItemWasEquipped);
            _item.UpdateItem(auxItemId);
            _item.SetEquipped(itemWasEquipped);
        }
        else
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            if (draggableItem.ItemId == 0) return;
            bool itemWasEquipped = draggableItem.IsEquipped;
            _item.UpdateItem(draggableItem.ItemId);
            _item.SetEquipped(itemWasEquipped);
            draggableItem.OnEndDrag(eventData);
            draggableItem.RemoveItem();
        }
    }

    void Start()
    {
        _item = GetComponentInChildren<DraggableItem>();
    }

    public void UpdateItem(int itemId)
    {
        _item.UpdateItem(itemId);
    }
}
