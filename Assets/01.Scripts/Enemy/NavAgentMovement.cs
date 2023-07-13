using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetInitData(float speed)
    {
        _navMeshAgent.speed = speed;
        _navMeshAgent.isStopped = false;
    }

    public bool CheckIsArrived()
    {
        if (_navMeshAgent.pathPending == false && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) { return true; }
        else { return false; }
    }

    public void StopImmediately()
    {
        _navMeshAgent.SetDestination(transform.position);
    }

    public void MoveToTarget(Vector3 pos)
    {
        _navMeshAgent.SetDestination(pos);
    }

    public void ResetNavAgent()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
    }
}
