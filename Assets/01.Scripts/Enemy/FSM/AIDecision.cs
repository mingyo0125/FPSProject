using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecision : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected EnemyController _enemyController;

    public bool IsReverse = false;

    public virtual void SetUp(Transform parentRoot) //state -> transitions -> Decision 순서롤 셋업을 해준다.
    {
        _enemyController = parentRoot.GetComponent<EnemyController>();
        _aiActionData = parentRoot.Find("AI").GetComponent<AIActionData>(); 
    }
}
