using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    private List<AIDecision> _aiDecisions;

    [SerializeField]
    private CommonAIState _nextState;
    public CommonAIState NextState => _nextState;

    public void SetUp(Transform parentRoot)
    {
        _aiDecisions = new List<AIDecision>();
        GetComponents<AIDecision>(_aiDecisions);

        _aiDecisions.ForEach(d => d.SetUp(parentRoot)); //state -> transitions -> Decision 순서롤 셋업을 해준다.
    }

    public bool CheckDecisions()
    {
        bool result = false;
        foreach(AIDecision decision in _aiDecisions)
        {
            result = decision.MakeDecision();

            if (decision.IsReverse) { result = !result; }
            if (result == false) { break; }
        }
        return result;
    }
}
