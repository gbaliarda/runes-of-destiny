using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{
    #region SINGLETON
    static public Player instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        base.Awake();
        instance = this;
    }
    #endregion

    #region PROPERTIES
    [SerializeField] private ParticleSystem _clickEffect;
    [SerializeField] private GameObject _inventory;
    private RaycastHit _hit;
    [SerializeField] private LayerMask _clickableLayers;
    [SerializeField] public InventorySlot[] inventory;

    private Database _database;

    #endregion

    #region KEY_BINDINGS
    [SerializeField] private KeyCode _move = KeyCode.Mouse1;
    [SerializeField] private KeyCode _firstAbility = KeyCode.Q;
    [SerializeField] private KeyCode _secondAbility = KeyCode.W;
    [SerializeField] private KeyCode _thirdAbility = KeyCode.E;
    [SerializeField] private KeyCode _fourthAbility = KeyCode.R;
    [SerializeField] private KeyCode _healthPot = KeyCode.D;
    [SerializeField] private KeyCode _manaPot = KeyCode.F;
    [SerializeField] private KeyCode _openInventory = KeyCode.I;

    #endregion

    protected new void Start()
    {
        base.Start();
        _database = new Database();
        _database.InitializeDatabase();

        if (_inventory == null) _inventory = GameObject.Find("Inventory");
        inventory = FindObjectsOfType<InventorySlot>();
        inventory = inventory.OrderBy(slot => slot.name).ToArray();

        UserData userData = _database.GetUser(1);
        
        if (userData != null)
        {
            int inventoryIndex = 0;
            foreach(int itemId in userData.inventory)
            {
                if (inventoryIndex <  inventory.Length) inventory[inventoryIndex++].UpdateItem(itemId);
            }
        }


        EventsManager.instance.OnGameOver += OnGameOver;
        EventsManager.instance.OnOpenInventory += OnOpenInventory;
        EventsManager.instance.OnEquippedItem += OnEquippedItem;
        EventsManager.instance.OnPickedUpItem += OnPickedUpItem;
        Debug.Log($"Inventory slots: {inventory.Length}");
    }

    private void UseRune(int runeIndex)
    {
        if (attackController == null) return;
        if (attackController.Runes[runeIndex].CooldownLeft > 0) return;
        if (attackController.Runes[runeIndex].RuneStats.ManaCost > mana) return;
        EventQueueManager.instance.AddCommand(new CmdSpendMana(this, attackController.Runes[runeIndex].RuneStats.ManaCost));
        attackController.Runes[runeIndex].SetCooldown(attackController.Runes[runeIndex].RuneStats.Cooldown);
        attackController.AttackOnMousePosition(runeIndex);
        EventsManager.instance.EventAbilityUse(runeIndex, attackController.Runes[runeIndex].RuneStats.Cooldown);
    }

    private new void Update()
    {
        if (isDead || isGameOver) return;
        base.Update();
        if (Input.GetKeyDown(_firstAbility)) UseRune(0);
        if (Input.GetKeyDown(_secondAbility)) UseRune(1);
        if (Input.GetKeyDown(_thirdAbility)) UseRune(2);
        if (Input.GetKeyDown(_fourthAbility)) UseRune(3);
        if (Input.GetKeyDown(_healthPot) && healthPotionController != null) healthPotionController.Heal();
        if (Input.GetKeyDown(_manaPot) && manaPotionController != null) manaPotionController.GetMana();
        if (Input.GetKeyDown(_openInventory)) EventsManager.instance.EventOpenInventory(!_inventory.activeSelf);


        if (Input.GetKeyDown(_move))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, 100f, _clickableLayers))
            {
                if (movementController != null) movementController.Move(_hit.point);

                if (_clickEffect != null) ClickEffect();
            }
        }
    }

    private void OnGameOver(bool isVictory)
    {
        base.OnGameOver(isVictory);
        if (isVictory) EventQueueManager.instance.AddCommand(new CmdPlayAnimation(animator, "Victory"));
    }

    private void OnOpenInventory(bool isOpen)
    {
        _inventory.SetActive(isOpen);
    }

    public void ApplyItem(DraggableItem item)
    {
        if (isDead) return;
        if (!item.IsEquipped)
        {
            Debug.Log($"Adding stats {item.ItemStats.MaxLife}");
            characterStats.AddStats(item.ItemStats);
        }
        else
            characterStats.RemoveStats(item.ItemStats);

        if (item.ItemStats.MovementSpeed != 0)
        {
            movementController.SetSpeed(characterStats.MovementSpeed);
        }

        if (item.ItemStats.MaxLife > 0)
        {
            if (life > characterStats.MaxLife) life = characterStats.MaxLife;
            EventsManager.instance.EventTakeDamage(life);
            EventsManager.instance.EventTargetHealthChange(gameObject.GetInstanceID(), life > 0 ? life : 0, MaxLife);
        }

        if (item.ItemStats.MaxMana > 0)
        {
            if (mana > characterStats.MaxMana) mana = characterStats.MaxMana;
            EventsManager.instance.EventSpendMana(mana);
        }
    }

    private void OnEquippedItem(DraggableItem item)
    {
        Debug.Log($"{(item.IsEquipped ? "unequipped" : "equiped")} item");
        if (item.IsEquipped)
        {
            ApplyItem(item);
        } else
        {
            foreach(InventorySlot inventorySlot in inventory)
            {
                DraggableItem itemInInventory = inventorySlot.Item;
                if (itemInInventory == null) continue;
                if (itemInInventory.IsEquipped && itemInInventory.ItemType == item.ItemType)
                {
                    ApplyItem(itemInInventory);
                    itemInInventory.UnequipItem();
                }
            }
            ApplyItem(item);
        }
    }

    private void OnPickedUpItem(PickableItem item)
    {
        foreach(InventorySlot inventorySlot in inventory)
        {
            DraggableItem itemInInventory = inventorySlot.Item;
            if (itemInInventory == null) continue;
            if (itemInInventory.ItemId != 0) continue;

            inventorySlot.UpdateItem(item.ItemId);
            item.DestroyItem();
            break;
        }
    }

    private void DropItem()
    {

    }

    public override void Die()
    {
        if (movementController != null) movementController.Move(transform.position);
        EventQueueManager.instance.AddCommand(new CmdPlayAnimation(animator, "Dead"));
        isDead = true;
        EventsManager.instance.EventGameOver(false);
    }

    private void ClickEffect()
    {
        GameObject effectContainer = new GameObject("ClickEffectContainer");

        effectContainer.transform.position = _hit.point + new Vector3(0, 0.1f, 0);

        ParticleSystem effectInstance = Instantiate(_clickEffect, effectContainer.transform);

        effectInstance.transform.localPosition = Vector3.zero;

        Destroy(effectContainer, 2.0f);
    }

    public override int HealDamage(int damage)
    {
        base.HealDamage(damage);
        EventsManager.instance.EventTakeDamage(life);
        return life;
    }

    public override void GetMana(int newMana)
    {
        base.GetMana(newMana);
        EventsManager.instance.EventSpendMana(mana);
    }

    public override int TakeDamage(DamageStatsValues damage)
    {
        base.TakeDamage(damage);
        EventsManager.instance.EventTakeDamage(life);
        return life;
    }

    public override void SpendMana(int manaCost)
    {
        base.SpendMana(manaCost);
        EventsManager.instance.EventSpendMana(mana);
    }
}
