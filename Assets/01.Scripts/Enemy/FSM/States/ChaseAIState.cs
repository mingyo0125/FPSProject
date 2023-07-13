using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIState : CommonAIState
{
    public override void OnEnterState()
    {
        Debug.Log("ChaseStateEnter");
    }

    public override void OnExitState()
    {
        Debug.Log("ChaseStateExit");
    }

    private void FootStepHandle()
    {

    }

    public override bool UpdateState()
    {
        _enemyController.NavMeshAgent.MoveToTarget(_aiActionData.LastSpotPoint);
        _aiActionData.IsArrived = _enemyController.NavMeshAgent.CheckIsArrived();

        return base.UpdateState();
    }
}
