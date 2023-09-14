using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    IRune Rune { get; }
    void Attack(Vector3 direction);

    void AttackOnMousePosition();
}
