using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _targetTrm;
    public Transform TargetTrm => _targetTrm;

    private CommonAIState _initState;
    private AIActionData _actionData;

    private List<AITransition> _anyTransitions = new List<AITransition>();
    public List<AITransition> AnyTransitions => _anyTransitions;

    protected virtual void Awake()
    {
        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        //�� state�� ���� �¾�
        states.ForEach(s => s.SetUp(transform)); //state -> transitions -> Decision ������ �¾��� ���ش�.
    }
}
