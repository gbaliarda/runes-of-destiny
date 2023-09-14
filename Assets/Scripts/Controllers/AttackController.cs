using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AttackController : MonoBehaviour, INavMovable, IAttackable
{
    public NavMeshAgent Agent => _agent;
    private NavMeshAgent _agent;

    public IRune Rune => _rune;
    [SerializeField] private Rune _rune;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Attack(Vector3 direction)
    {
        Move(transform.position);
        _rune.ShootAtDirection(direction);
    }

    public void AttackOnMousePosition()
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);
        Debug.Log(hit.point);
        Attack(hit.point);
    }

    public void Move(Vector3 position)
    {
        _agent.SetDestination(position);
    }
}
