using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : PoolableMono
{
    [SerializeField]
    private CommonAIState _currentState;
    public CommonAIState CurrentState => _currentState;

    [SerializeField]
    private SpiderDataSO _spiderDataSO;
    public SpiderDataSO SpiderDataSO => _spiderDataSO;

    private Transform _targetTrm;
    public Transform TargetTrm => _targetTrm;

    private NavAgentMovement _navMeshAgent;
    public NavAgentMovement NavMeshAgent => _navMeshAgent;

    private AIActionData _actionData;

    private List<AITransition> _anyTransitions = new List<AITransition>();
    public List<AITransition> AnyTransitions => _anyTransitions;

    private void Start()
    {
        _navMeshAgent.SetInitData(_spiderDataSO.MoveSpeed);

        _targetTrm = GameManager.Instance.PlayerTrm; //������ ������ ���Ǿ�� ���߿� ����
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavAgentMovement>();

        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        //�� state�� ���� �¾�
        states.ForEach(s => s.SetUp(transform)); //state -> transitions -> Decision ������ �¾��� ���ش�.

    }

    public void ChangeState(CommonAIState nextstate)
    {
        _currentState?.OnExitState();
        _currentState = nextstate;
        _currentState?.OnEnterState();
    }

    public override void Init()
    {
        ChangeState(_currentState);
        _actionData.Init();
    }

}
