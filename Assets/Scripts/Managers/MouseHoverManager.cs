using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverManager : MonoBehaviour
{
    static public MouseHoverManager instance;
    [SerializeField] private TargetHealthBar _targetHpBar;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    void Start()
    {
        Cursor.visible = true;
        _targetHpBar.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void OnTargetDamageableEnter(int instanceId, int life, int maxLife, string name)
    {
        _targetHpBar.gameObject.SetActive(true);
        _targetHpBar.SubscribeToTarget(instanceId, name);
        EventsManager.instance.EventTargetHealthChange(instanceId, life, maxLife);
    }

    public void OnTargetDamageableExit()
    {
        _targetHpBar.gameObject.SetActive(false);
        _targetHpBar.UnSubscribeToTarget();
    }
}
