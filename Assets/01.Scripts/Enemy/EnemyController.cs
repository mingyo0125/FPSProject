using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Events;
using System.ComponentModel;
using UnityEngine.UI;

public class EnemyController : PoolableMono, IDamageAble
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

    private float hp;
    public float HP => hp;

    private float damage;
    public float Damage => damage;

    private void Start()
    {
        _navMeshAgent.SetInitData(_spiderDataSO.MoveSpeed);

        hp = _spiderDataSO.MaxHP;
        damage = _spiderDataSO.AtkDamage;

        _targetTrm = GameManager.Instance.PlayerTrm;
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

        _collider = transform.Find("Visual/Polygonal Metalon").GetComponent<MeshCollider>();
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

    public void OnDamage(float _damage)
    {
        int damage = Mathf.RoundToInt(Random.Range(_damage, 10));
        
        hp -= damage;

        PopUpText text = PoolManager.Instance.Pop("PopUpText") as PopUpText;
        text.transform.SetParent(gameObject.transform);
        text.TextSetUp(damage);

        Debug.Log(hp);

        if(hp <= 0)
        {
            _agentAnimator.OnAnimationEndTrigger += Die;
            _agentAnimator.SetDie();
            _navMeshAgent.NavMeshAgent.isStopped = true;
            _collider.enabled = false;
        }
    }

    private void Die()
    {
        Debug.Log("Die");
        onDieEvnet?.Invoke();
    }

    public void AfterDissolve()
    {
        _agentAnimator.OnAnimationEndTrigger -= Die;
        PoolManager.Instance.Push(this);

    }

}
