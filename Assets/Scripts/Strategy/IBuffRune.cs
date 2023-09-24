using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffRune
{
    GameObject BuffPrefab { get; }
    Transform BuffContainer { get; }
    CharacterStats BuffStats { get; }
    Character Player { get; }
    float Duration { get; }
    float CooldownLeft { get; }

    void Buff();

    void SetCooldown(float cooldown);
}
