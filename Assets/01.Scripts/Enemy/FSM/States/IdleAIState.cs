using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAIState : CommonAIState
{
    public override void OnEnterState()
    {
        Debug.Log("IdleStateEnter");
    }

    public override void OnExitState()
    {
        Debug.Log("IdleStateExit");
    }

    public override bool UpdateState()
    {
        return base.UpdateState();
    }
}
