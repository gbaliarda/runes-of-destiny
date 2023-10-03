using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    IBuffRune Owner { get; }
    float Duration { get; }

    void Init();
    void Die();
    void SetOwner(BuffRune owner);
}
