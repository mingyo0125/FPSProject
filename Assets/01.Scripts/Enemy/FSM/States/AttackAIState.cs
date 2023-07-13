using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    public override void OnEnterState()
    {
        Debug.Log("AttackStateEnter");
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
