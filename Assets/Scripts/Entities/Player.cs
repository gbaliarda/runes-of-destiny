using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region PROPERTIES
    [SerializeField] private ParticleSystem _clickEffect;
    [SerializeField] private GameObject _inventory;
    private RaycastHit _hit;
    [SerializeField] private LayerMask _clickableLayers;
    private bool _gameOver;

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
        if (_inventory == null) _inventory = GameObject.Find("Inventory");
        EventsManager.instance.OnGameOver += OnGameOver;
        EventsManager.instance.OnOpenInventory += OnOpenInventory;
    }

    private void UseRune(int runeIndex)
    {
        if (attackController == null) return;
        if (attackController.Runes[runeIndex].CooldownLeft > 0) return;
        if (attackController.Runes[runeIndex].RuneStats.ManaCost > mana) return;
        SpendMana(attackController.Runes[runeIndex].RuneStats.ManaCost);
        attackController.Runes[runeIndex].SetCooldown(attackController.Runes[runeIndex].RuneStats.Cooldown);
        attackController.AttackOnMousePosition(runeIndex);
        EventsManager.instance.EventAbilityUse(runeIndex, attackController.Runes[runeIndex].RuneStats.Cooldown);
    }

    private new void Update()
    {
        if (isDead || _gameOver) return;
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
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, 100f, _clickableLayers))
            {
                if (movementController != null) movementController.Move(_hit.point);

                if (_clickEffect != null) ClickEffect();
            }
        }
    }

    private void OnGameOver(bool isVictory)
    {
        Debug.Log($"Victory: {isVictory}");
        _gameOver = true;
        if (isVictory) animator.Play("Victory");
    }

    private void OnOpenInventory(bool isOpen)
    {
        _inventory.SetActive(isOpen);
    }

    public override void Die()
    {
        base.Die();
    }

    private void ClickEffect()
    {
        GameObject effectContainer = new GameObject("ClickEffectContainer");

        effectContainer.transform.position = _hit.point + new Vector3(0, 0.1f, 0);

        ParticleSystem effectInstance = Instantiate(_clickEffect, effectContainer.transform);

        effectInstance.transform.localPosition = Vector3.zero;

        Destroy(effectContainer, 2.0f);
    }

    public override int TakeDamage(int damage)
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

    public override void AbilityCasted(int runeIndex)
    {
        if (attackController == null) return;
        SpendMana(attackController.Runes[runeIndex].RuneStats.ManaCost);
        EventsManager.instance.EventAbilityUse(runeIndex, attackController.Runes[runeIndex].RuneStats.Cooldown);
    }

    protected override IEnumerator ManaRegenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (mana < characterStats.MaxMana)
            {
                mana += characterStats.ManaRegen;
                if (mana > characterStats.MaxMana) mana = characterStats.MaxMana;
                EventsManager.instance.EventSpendMana(mana);
            }
        }
    }
}
