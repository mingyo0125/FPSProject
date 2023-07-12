using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAIState : MonoBehaviour, IState
{
    protected List<AITransition> _transitions;
    protected EnemyController _enemyController;
    protected AIActionData _aiActionData;

    public virtual void OnEnterState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnExitState()
    {
        throw new System.NotImplementedException();
    }

    public virtual void SetUp(Transform agentRoot)
    {
        _enemyController = agentRoot.GetComponent<EnemyController>();
        _aiActionData = agentRoot.Find("AI").GetComponent<AIActionData>();

        _transitions = new List<AITransition>();
        GetComponentsInChildren<AITransition>(_transitions);

        _transitions
    }

    public virtual bool UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
