using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdGetMana : ICommand
{
    private IMana _mana;
    private int _manaAmount;

    public CmdGetMana(IMana mana, int manaAmount)
    {
        _mana = mana;
        _manaAmount = manaAmount;
    }

    public void Execute() => _mana.GetMana(_manaAmount);

    public void Undo() => _mana.SpendMana(_manaAmount);
}
