using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public interface IManaPotion
{
    int ManaPotChargesLeft { get; }
    float CurrentManaPotCooldowm { get; }
    float CurrentChargeRegenerationCycle { get; }

    public void GetMana();
}
