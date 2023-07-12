using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    private List<AIDecision> _aiDecisions;

    public void SetUp(Transform parentRoot)
    {
        _aiDecisions = new List<AIDecision>();
        GetComponents<AIDecision>(_aiDecisions);

        _aiDecisions.ForEach(d => d.SetUp(parentRoot));
    }
}
