using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRune
{
    GameObject RunePrefab { get; }
    Transform RuneContainer { get; }
    RuneStats RuneStats { get; }
    Character Player { get; }
    float CooldownLeft { get; }

    void Shoot();

    void ShootAtDirection(Vector3 direction);

    void SetCooldown(float cooldown);
}
