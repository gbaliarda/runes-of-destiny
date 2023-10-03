using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdPlaySound : ICommand
{
    private IListener _listener;
    public IListener Listener => _listener;

    public CmdPlaySound(IListener listener)
    {
        this._listener = listener;
    }

    public void Execute() => _listener.PlayOneShot();

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
