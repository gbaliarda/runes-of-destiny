using System.Collections;
using System.Collections.Generic;
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

    public void TakeDamage(int damage)
    {
        _actor.TakeDamage(damage);
    }
}
