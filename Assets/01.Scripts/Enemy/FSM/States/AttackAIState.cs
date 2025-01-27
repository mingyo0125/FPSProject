using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    protected Vector3 _targetVec;
    protected bool isActive = false;

    int playerLayer = 1 << 6;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
    }

    public override void OnEnterState()
    {
        _enemyController.NavMeshAgent.StopImmediately();
        _enemyController.AgentAnimator.OnAnimationEvenTrigger += AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger += AttackAnimationEndHandle;
        isActive = true;

    }

    public override void OnExitState()
    {
        Debug.Log("AttackStateExit");

        _enemyController.AgentAnimator.OnAnimationEvenTrigger -= AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger -= AttackAnimationEndHandle;

        _enemyController.AgentAnimator.SetStackAttack(false);

        _aiActionData.IsAttacking = false;
        isActive = false;

    }

    private void AttackAnimationEndHandle()
    {
        _enemyController.AgentAnimator.SetStackAttack(false);

        StartCoroutine(DelayCoroutine(() =>  _aiActionData.IsAttacking = false, _enemyController.SpiderDataSO.MotionDelay));
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

    private void SetTarget()
    {
        _targetVec = _enemyController.TargetTrm.position - transform.position;
        _targetVec.y = 0;
    }

    public override bool UpdateState()
    {
        if (base.UpdateState()) { return true; }

        if (_aiActionData.IsAttacking == false && isActive)
        {
            _enemyController.AgentAnimator.SetStackAttack(true);
            _aiActionData.IsAttacking = true;

            Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, playerLayer);

            foreach (Collider collider in colliders)
            {
                if(collider.gameObject != null)
                {
                    Debug.Log(collider.name);

                    if (collider.transform.TryGetComponent(out FirstPersonShooterController player))
                    {
                        Debug.Log("때려보리기");
                        player.OnDamage(_enemyController.Damage);
                    }
                }
                
            }

        }

        return false;
    }
}
