using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIState : CommonAIState
{
    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
    }

    public override void OnEnterState()
    {
        Debug.Log("ChaseStateEnter");

        _enemyController.AgentAnimator.SetWalkForward(true);
    }

    public override void OnExitState()
    {
        _enemyController.AgentAnimator.SetWalkForward(false);
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
