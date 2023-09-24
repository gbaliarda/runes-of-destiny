using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    BuffRune Owner { get; }
    float Duration { get; }
    float LifeTime { get; }

    void Init();
    void Die();
    void SetOwner(BuffRune owner);
    void ReduceDuration(float duration);
}
