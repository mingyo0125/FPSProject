using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    SpiderDataSO _spiderDataSO;

    protected bool isActive = false;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _spiderDataSO = _enemyController.SpiderDataSO;
    }

    public override void OnEnterState()
    {
        _enemyController.NavMeshAgent.StopImmediately();
        _enemyController.AgentAnimator.OnAnimationEvenTrigger += AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger += AttackAnimationEndHandle;
        isActive = true;

        Debug.Log("AttackStateEnter");
    }

    private void AttackAnimationEndHandle()
    {
        StartCoroutine(DelayCoroutine(() =>  _aiActionData.IsAttacking = false, _spiderDataSO.MotionDelay));
    }

    private IEnumerator DelayCoroutine(Action Callback, float time)
    {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }

    private void AttackCollisionHandle()
    {
        Debug.Log("Attack");
    }

    public override void OnExitState()
    {
        Debug.Log("AttackStateExit");

        _enemyController.AgentAnimator.OnAnimationEvenTrigger -= AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger -= AttackAnimationEndHandle;

        isActive = false;

    }

    public override bool UpdateState()
    {
        //if (base.UpdateState()) { return true; }

        if(_aiActionData.IsAttacking == false && isActive)
        {
            _enemyController.AgentAnimator.SetStackAttack();
            _aiActionData.IsAttacking = true;
        }

        return base.UpdateState();

    }
}
