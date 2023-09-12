using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMovement : ICommand
{
    private Transform _transform;
    private Vector3 _direction;
    private float _speed;
    public CmdMovement(Transform transform, Vector3 direction, float speed)
    {
        _transform = transform;
        _direction = direction;
        _speed = speed;
    }

    public void Execute() => _transform.Translate(_speed * Time.deltaTime * _direction);

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
