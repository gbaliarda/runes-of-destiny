using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTooltipManager : MonoBehaviour
{
    #region SINGLETON
    static public ItemTooltipManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;

    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void SetAndShowTooltip(DraggableItem item)
    {
        gameObject.SetActive(true);
        // transform.position = item.transform.position;
        _itemName.text = item.Name;
        _itemDescription.text = $"{(item.ItemStats.MaxLife > 0 ? $"+ {item.ItemStats.MaxLife} Health \n" : "")}" +
                                $"{(item.ItemStats.MaxMana > 0 ? $"+ {item.ItemStats.MaxMana} Mana \n" : "")}" +
                                $"{(item.ItemStats.Armor > 0 ? $"+ {item.ItemStats.Armor} Armor \n" : "")}" +
                                $"{(item.ItemStats.MovementSpeed > 0 ? $"+ {item.ItemStats.MovementSpeed} Movement Speed \n" : "")}" +
                                $"{(item.ItemStats.MovementSpeed < 0 ? $"- {item.ItemStats.MovementSpeed} Movement Speed \n" : "")}";
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        _itemName.text = string.Empty;
        _itemDescription.text = string.Empty;
    }
}
