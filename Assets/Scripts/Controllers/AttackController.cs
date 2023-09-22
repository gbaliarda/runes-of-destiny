using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackController : MonoBehaviour, INavMovable, IAttackable
{
    public NavMeshAgent Agent => _agent;
    private NavMeshAgent _agent;

    public IRune[] Runes => _runes;
    [SerializeField] private Rune[] _runes;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Attack(int runeIndex, Vector3 direction)
    {
        Move(transform.position);
        _runes[runeIndex].ShootAtDirection(direction);
    }

    public void AttackOnMousePosition(int runeIndex)
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);
        Attack(runeIndex, hit.point);
    }

    public void Move(Vector3 position)
    {
        if (_agent != null) _agent.SetDestination(position);
    }
}
