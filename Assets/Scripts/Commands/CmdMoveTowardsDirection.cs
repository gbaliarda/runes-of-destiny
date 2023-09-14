using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CmdMoveTowardsDirection : ICommand
{
    private Transform _transform;
    private Vector3 _direction;
    private float _distanceToMove = 15f;
    private float _speed;
    public CmdMoveTowardsDirection(Transform transform, Vector3 direction, float speed)
    {
        _transform = transform;
        _direction = direction;
        _speed = speed;
    }

    public void Execute()
    {
        if (_transform == null) return;

        Vector3 destination = _transform.position + _direction * _distanceToMove;

        _transform.position = Vector3.MoveTowards(_transform.position, destination, _speed * Time.deltaTime);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
