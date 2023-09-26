using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(SkinnedMeshRenderer))]
public class Body : MonoBehaviour
{
    [SerializeField] private Actor _actor;

    public Actor Actor => _actor;
    void Awake()
    {
        _actor = transform.parent.GetComponent<Actor>();
    }

    public void TakeDamage(DamageStatsValues damage)
    {
        _actor.TakeDamage(damage);
    }

    private void OnMouseEnter()
    {
        MouseHoverManager.instance.OnTargetDamageableEnter(_actor.gameObject.GetInstanceID(), _actor.Life, _actor.MaxLife, _actor.gameObject.name);
    }

    private void OnMouseExit()
    {
        MouseHoverManager.instance.OnTargetDamageableExit();
    }
}
