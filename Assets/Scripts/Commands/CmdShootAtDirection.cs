using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdShootAtDirection : ICommand
{
    private Rune _rune;
    public Rune Rune => _rune;

    private Vector3 _direction;
    public Vector3 Direction => _direction;


    public CmdShootAtDirection(Rune rune, Vector3 direction)
    {
        this._rune = rune;
        this._direction = direction;
    }

    public void Execute() => _rune.ShootAtDirection(_direction);

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
