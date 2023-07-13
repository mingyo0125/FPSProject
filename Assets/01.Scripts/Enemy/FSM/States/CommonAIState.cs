using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonAIState : MonoBehaviour, IState
{
    protected List<AITransition> _transitions;

    protected EnemyController _enemyController;
    protected AIActionData _aiActionData;

    public abstract void OnEnterState();

    public abstract void OnExitState();

    public virtual void SetUp(Transform agentRoot)
    {
        _enemyController = agentRoot.GetComponent<EnemyController>();
        _aiActionData = agentRoot.Find("AI").GetComponent<AIActionData>();

        _transitions = new List<AITransition>();
        GetComponentsInChildren<AITransition>(_transitions);

        _transitions.ForEach(t => t.SetUp(agentRoot));
    }

    public virtual bool UpdateState()
    {
        foreach(AITransition transition in _transitions)
        {
            if(transition.CheckDecisions())
            {
                _enemyController.ChangeState(transition.NextState);
                return true;
            }
        }
        return false;
    }
}
