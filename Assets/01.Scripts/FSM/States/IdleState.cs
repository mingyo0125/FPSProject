using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
{
    private float range =  5;

    public void EnterState(AIBrain AiBrain)
    {
        Debug.Log("EnterIdleState");
    }

    public void UpdateState(AIBrain AiBrain)
    {
        if(Vector3.Distance(gameObject.transform.position, GameManager.Instance.PlayerTrm.position) <= range)
        {
            AiBrain.ChangeState(AiBrain.ChaseStateV);
        }
    }
}
