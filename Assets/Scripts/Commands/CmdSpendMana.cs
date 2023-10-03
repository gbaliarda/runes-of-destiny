using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdSpendMana : ICommand
{
    private IMana _mana;
    private int _manaAmount;

    public CmdSpendMana(IMana mana, int manaAmount)
    {
        _mana = mana;
        _manaAmount = manaAmount;
    }

    public void Execute() => _mana.SpendMana(_manaAmount);

    public void Undo() => _mana.GetMana(_manaAmount);
}
