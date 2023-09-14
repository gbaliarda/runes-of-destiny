using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region PROPERTIES
    [SerializeField] private ParticleSystem _clickEffect;
    [SerializeField] private GameObject _inventory;
    private RaycastHit _hit;
    private string groundTag = "Ground";
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
        if (Input.GetKeyDown(_shootAttack))
        {
            basicAttack.Shoot();
        }

        if (Input.GetKeyDown(_openInventory)) EventsManager.instance.EventOpenInventory(!_inventory.activeSelf);

        if (Input.GetKeyDown(KeyCode.Alpha1)) EventsManager.instance.EventGameOver(true);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EventsManager.instance.EventGameOver(false);

        if (Input.GetKeyDown(_move))
        {
            if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition), out _hit) && _hit.collider.CompareTag(groundTag))
            {
                new CmdMoveToClick(agent, _hit).Execute();
                if (_clickEffect != null)
                {
                    GameObject effectContainer = new GameObject("ClickEffectContainer");

                    effectContainer.transform.position = _hit.point + new Vector3(0, 0.1f, 0);

                    ParticleSystem effectInstance = Instantiate(_clickEffect, effectContainer.transform);

                    effectInstance.transform.localPosition = Vector3.zero;

                    Destroy(effectContainer, 2.0f);
                }
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
        EventsManager.instance.EventGameOver(false);
        base.Die();
    }
}
