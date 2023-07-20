using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    PoolingListSO _poolingListSO;

    public GameState State { get; private set; }

    private readonly List<IGameComponents> _components = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;
        MakePool();
    }

    private void Start()
    {
        UpdateState(GameState.Init);
    }

    public void UpdateState(GameState state)
    {
        State = state;

        foreach (var component in _components)
        {
            component.UpdateState(state);
        }

        if (state == GameState.Init) { UpdateState(GameState.Playing); }

    }

    public T GetGameComponent<T>() where T : GameComponent
    {
        var component = default(T);

        foreach(var gamecomponents in _components)
        {
            if (gamecomponents is not T tcomponent) { continue; }

            component = tcomponent;

            break;
        }

        return component;
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingListSO.List.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount)); //리스트에 있는 모든
    }



    #region PlayerTrm

    private Transform _playerTrm;
    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
            {
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _playerTrm;
        }
    }

    #endregion

}
