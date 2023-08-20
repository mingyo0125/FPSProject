using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class EnemyController : PoolableMono, IDamage
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
    
    private AgentAnimator _agentAnimator;
    public AgentAnimator AgentAnimator => _agentAnimator;

    private AIActionData _actionData;

    private List<AITransition> _anyTransitions = new List<AITransition>();
    public List<AITransition> AnyTransitions => _anyTransitions;

    public UnityEvent onDieEvnet;
    private MeshCollider _collider;
    float hp;

    private void Start()
    {
        _navMeshAgent.SetInitData(_spiderDataSO.MoveSpeed);

        _targetTrm = GameManager.Instance.PlayerTrm; //범위는 오버랩 스피어로 나중에 지정
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavAgentMovement>();
        _agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();

        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        //각 state에 대한 셋업
        states.ForEach(s => s.SetUp(transform)); //state -> transitions -> Decision 순서롤 셋업을 해준다.

        hp = _spiderDataSO.MaxHP;
        _collider = transform.Find("Visual/Polygonal Metalon").GetComponent<MeshCollider>();
        _agentAnimator.OnAnimationEndTrigger += Die;
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

    public void OnDamage(float damage)
    {
        hp -= damage;

        Debug.Log(hp);

        if(hp <= 0)
        {

            _agentAnimator.SetDie();
            _collider.enabled = false;
        }
    }

    private void Die()
    {
        onDieEvnet?.Invoke();
    }

    public void GotoPool()
    {
        PoolManager.Instance.Push(this);
    }
}
