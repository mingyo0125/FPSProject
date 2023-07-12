using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    IState _currentState;

    public IdleState IdleStateV;
    public ChaseState ChaseStateV;
    public AttackState AttackStateV;

    private void Start()
    {
        _currentState = IdleStateV;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void ChangeState(IState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
}
