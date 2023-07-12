using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecision : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected EnemyController _enemyController;

    public bool IsReverse = false;

    public virtual void SetUp(Transform parentRoot)
    {
        _enemyController = parentRoot.GetComponent<EnemyController>();
        _aiActionData = parentRoot.Find("AI").GetComponent<AIActionData>();
    }
}
