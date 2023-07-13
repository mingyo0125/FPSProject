using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    public override void OnEnterState()
    {
        _enemyController.NavMeshAgent.StopImmediately();
        _enemyController.AgentAnimator.OnAnimationEvenTrigger += AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger += AttackAnimationEndHandle;
        _enemyController.AgentAnimator.SetStackAttack();
        Debug.Log("AttackStateEnter");
    }

    private void AttackAnimationEndHandle()
    {
        _enemyController.AgentAnimator.OnAnimationEvenTrigger -= AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger -= AttackAnimationEndHandle;


    }

    private void AttackCollisionHandle()
    {
        Debug.Log("Attack");
    }

    public override void OnExitState()
    {
        Debug.Log("AttackStateExit");
    }

    public override bool UpdateState()
    {
        return base.UpdateState();
    }
}
