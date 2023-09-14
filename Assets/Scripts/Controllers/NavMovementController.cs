using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMovementController : MonoBehaviour, INavMovable
{

    public NavMeshAgent Agent => _agent;
    private NavMeshAgent _agent;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 position) => EventQueueManager.instance.AddCommand(new CmdMoveToPosition(_agent, position));

}
