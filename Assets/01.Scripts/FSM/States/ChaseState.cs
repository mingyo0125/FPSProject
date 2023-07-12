using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour, IState
{
    public void EnterState(AIBrain AiBrain)
    {
        Debug.Log("ChaseState");
    }

    public void UpdateState(AIBrain AiBrain)
    {

    }
}
