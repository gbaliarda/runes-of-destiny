using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdChangeRotation : ICommand
{
    private Transform _transform;
    public Transform Transform => _transform;

    private Vector3 _direction;
    public Vector3 Direction => _direction;

    public CmdChangeRotation(Transform transform, Vector3 direction)
    {
        _transform = transform;
        _direction = direction;
    }

    public void Execute()
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));
        _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
