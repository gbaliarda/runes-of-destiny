using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviour, IMovable
{

    public float MovementSpeed => _movementSpeed;
    [SerializeField] private float _movementSpeed = 10f;

    public float TurnSpeed => _turnSpeed;
    [SerializeField] private float _turnSpeed = 100f;

    public void Move(Vector3 direction) => transform.Translate(MovementSpeed * Time.deltaTime * direction);

    public void Turn(Vector3 direction) => transform.Rotate(TurnSpeed * Time.deltaTime * direction, Space.Self);
}
