using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFrostBall
{
    IRune Owner { get; }
    float Speed { get; }
    float LifeTime { get; }
    Collider Collider { get; }
    Rigidbody Rb { get; }

    void Init();
    void Travel();
    void Die();
    void SetOwner(IRune owner);
}
