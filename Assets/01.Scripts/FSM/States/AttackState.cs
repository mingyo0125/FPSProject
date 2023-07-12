using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    public void EnterState(AIBrain AiBrain)
    {
        Debug.Log("AttackIdleState");
    }

    public void UpdateState(AIBrain AiBrain)
    {

    }
}
