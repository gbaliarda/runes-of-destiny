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
                                $"{(item.ItemStats.MovementSpeed < 0 ? $"- {item.ItemStats.MovementSpeed} Movement Speed \n" : "")}" +
                                $"{(item.ItemStats.EvasionChance > 0 ? $"+ {item.ItemStats.EvasionChance}% Evasion Chance \n" : "")}" +
                                $"{(item.ItemStats.WaterResistance > 0 ? $"+ {item.ItemStats.WaterResistance }% Water Resistance \n" : "")}" +
                                $"{(item.ItemStats.FireResistance > 0 ? $"+ {item.ItemStats.FireResistance }% Fire Resistance \n" : "")}" +
                                $"{(item.ItemStats.LightningResistance > 0 ? $"+ {item.ItemStats.LightningResistance }% Lightning Resistance \n" : "")}" +
                                $"{(item.ItemStats.VoidResistance > 0 ? $"+ {item.ItemStats.VoidResistance}% Void Resistance \n" : "")}" +
                                $"{(item.ItemStats.HealthRegen > 0 ? $"+ {item.ItemStats.HealthRegen} HPS \n" : "")}" +
                                $"{(item.ItemStats.ManaRegen > 0 ? $"+ {item.ItemStats.ManaRegen} MPS \n" : "")}";
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        _itemName.text = string.Empty;
        _itemDescription.text = string.Empty;
    }
}
