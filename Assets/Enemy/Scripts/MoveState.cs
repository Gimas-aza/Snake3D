using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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