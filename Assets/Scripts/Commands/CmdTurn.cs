using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdTurn : ICommand
{
    private Transform _transform;
    private Vector3 _direction;
    private float _speed;
    public CmdTurn(Transform transform, Vector3 direction, float speed)
    {
        _transform = transform;
        _direction = direction;
        _speed = speed;
    }

    public void Execute() => _transform.Rotate(_speed * Time.deltaTime * _direction, Space.Self);

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}

