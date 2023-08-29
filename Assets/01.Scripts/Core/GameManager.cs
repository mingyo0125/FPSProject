using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    PoolingListSO _poolingListSO;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        AddManager();
        MakePool();
    }

    private void AddManager()
    {
        PoolManager.Instance = new PoolManager(transform);
        MapManager.Instance = GetComponent<MapManager>();
    }

    private void MakePool()
    {
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
