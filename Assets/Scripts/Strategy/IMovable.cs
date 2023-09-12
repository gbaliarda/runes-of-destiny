using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    float MovementSpeed { get; }
    float TurnSpeed { get; }
    void Move(Vector3 direction);
    void Turn(Vector3 direction);
}
