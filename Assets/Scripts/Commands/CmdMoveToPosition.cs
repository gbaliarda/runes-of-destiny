using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CmdMoveToPosition : ICommand
{
    private NavMeshAgent _agent;
    private Vector3 _position;
    public CmdMoveToPosition(NavMeshAgent agent, Vector3 position)
    {
        _agent = agent;
        _position = position;
    }

    public void Execute() 
    { 
        if(_agent != null) _agent.SetDestination(_position);
    }
        

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
