using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeSpell
{
    IRune Owner { get; }
    float LifeTime { get; }
    Collider Collider { get; }
    Rigidbody Rb { get; }

    void Init();
    void Die();
    void SetOwner(IRune owner);
}
