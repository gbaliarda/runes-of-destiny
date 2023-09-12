using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventQueueManager))]
public class Player : Character
{
    #region PROPERTIES
    [SerializeField] private ParticleSystem _clickEffect;
    private RaycastHit _hit;
    private string groundTag = "Ground";


    #endregion

    #region KEY_BINDINGS
    [SerializeField] private KeyCode _move = KeyCode.Mouse1;
    [SerializeField] private KeyCode _shootAttack = KeyCode.Q;

    #endregion

    new void Update()
    {
        if (isDead) return;
        base.Update();
        if (Input.GetKeyDown(_shootAttack))
        {
            basicAttack.Shoot();
        }

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
}
