using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMana
{
    int MaxMana { get; }
    int Mana { get; }
    void SpendMana(int mana);
    void GetMana(int mana);
}
