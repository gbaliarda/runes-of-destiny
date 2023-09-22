using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public interface IHealthPotion
{
    int HpPotChargesLeft { get; }
    float CurrentHpPotCooldowm { get; }
    float CurrentChargeRegenerationCycle { get; }

    public void Heal();
}
