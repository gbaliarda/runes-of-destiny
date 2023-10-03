using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdBuff : ICommand
{
    private IBuffable _buffable;
    private IBuff _buff;

    public CmdBuff(IBuffable buffable, IBuff buff)
    {
        _buffable = buffable;
        _buff = buff;
    }

    public void Execute() => _buffable.AddBuff(_buff);

    public void Undo() => _buffable.RemoveBuff(_buff);
}
