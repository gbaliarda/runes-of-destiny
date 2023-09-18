using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    IRune[] Runes { get; }
    void Attack(int runeIndex, Vector3 direction);

    void AttackOnMousePosition(int runeIndex);
}
