using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField]
    private float _distance = 5f;

    public override bool MakeDecision()
    {
        if (_enemyController.TargetTrm == null) { return false; } //�ȵǸ� true�� ����

        float distacne = Vector3.Distance(_enemyController.TargetTrm.position, transform.position);

        if(distacne <= _distance)
        {
            _aiActionData.LastSpotPoint = _enemyController.TargetTrm.position;
            _aiActionData.TargetSpotted = true;
        }
        else
        {
            _aiActionData.TargetSpotted = false;
        }

        return _aiActionData.TargetSpotted;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Color old = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _distance);
            Gizmos.color = old;
        }
    }
#endif

}
