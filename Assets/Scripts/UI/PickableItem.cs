using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Enums;

public class PickableItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;
    private int _itemId;
    private Rarity _rarity;
    private Database _database;

    public TextMeshProUGUI Name => _name;
    public int ItemId => _itemId;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        EventsManager.instance.EventPickedUpItem(this);
    }

    void Start()
    {
        _name = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponent<Image>();
        _database = new Database();
    }

    public void SetItem(int itemId)
    {
        Start();
        ItemData itemData = _database.GetItem(itemId);
        if (itemData != null)
        {
            _itemId = itemId;
            _name.text = itemData.Name;
            _rarity = itemData.Rarity;
            Debug.Log(_rarity);
            switch(_rarity)
            {
                case Rarity.Common:
                    _name.color = new Color(255, 255, 255);
                    SoundManager.instance.PlayOneShot(Resources.Load<AudioClip>("Drops/AlertSound2"));
                    break;
                case Rarity.Uncommon:
                    _name.color = new Color(38, 255, 0);
                    SoundManager.instance.PlayOneShot(Resources.Load<AudioClip>("Drops/AlertSound3"));
                    break;
                case Rarity.Rare:
                    _name.color = new Color(255, 255, 0);
                    SoundManager.instance.PlayOneShot(Resources.Load<AudioClip>("Drops/AlertSound1"));
                    break;
                case Rarity.Legendary:
                    _name.color = new Color(236, 132, 0);
                    SoundManager.instance.PlayOneShot(Resources.Load<AudioClip>("Drops/AlertSound4"));
                    break;
                case Rarity.Mythic:
                    _name.color = new Color(255, 43, 204);
                    SoundManager.instance.PlayOneShot(Resources.Load<AudioClip>("Drops/AlertSound5"));
                    break;
            }
            gameObject.SetActive(true);
        }
    }

    public void DestroyItem()
    {
        GetComponent<FollowCanvas>()?.DestroyFollow();
        Destroy(gameObject);
    }
}
