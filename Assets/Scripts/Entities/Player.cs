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
    [SerializeField] private KeyCode _shootAttack = KeyCode.Q;
    [SerializeField] private KeyCode _openInventory = KeyCode.I;

    #endregion

    protected new void Start()
    {
        base.Start();
        if (_inventory == null) _inventory = GameObject.Find("Inventory");
        EventsManager.instance.OnGameOver += OnGameOver;
        EventsManager.instance.OnOpenInventory += OnOpenInventory;
    }

    private new void Update()
    {
        if (isDead || _gameOver) return;
        base.Update();
        if (Input.GetKeyDown(_shootAttack)) attackController.AttackOnMousePosition();
        if (Input.GetKeyDown(_openInventory)) EventsManager.instance.EventOpenInventory(!_inventory.activeSelf);


        if (Input.GetKeyDown(_move))
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, 100f, _clickableLayers))
            {
                movementController.Move(_hit.point);

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
}
