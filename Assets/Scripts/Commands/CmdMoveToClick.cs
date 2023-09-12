using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CmdMoveToClick : ICommand
{
    private NavMeshAgent _agent;
    private RaycastHit _hit;
    public CmdMoveToClick(NavMeshAgent agent, RaycastHit hit)
    {
        _agent = agent;
        _hit = hit;
    }

    public void Execute() 
    { 
        _agent.destination = _hit.point;
    }
        

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
