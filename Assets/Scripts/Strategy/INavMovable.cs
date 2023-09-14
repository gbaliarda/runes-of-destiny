using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface INavMovable
{
    NavMeshAgent Agent { get; }
    void Move(Vector3 position);
}
