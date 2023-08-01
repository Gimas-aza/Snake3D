using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveState : State
{
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable() 
    {
        _agent.isStopped = true;
    }

    private void Update()
    {
        _agent.SetDestination(Target.transform.position);
    }
}
