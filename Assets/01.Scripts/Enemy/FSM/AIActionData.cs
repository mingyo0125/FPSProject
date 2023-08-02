using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionData : MonoBehaviour
{
    public bool TargetSpotted; //���� �߰ߵǾ��°�?
    public Vector3 HitPoint; //���������� ���� ������ ����ΰ�?
    public Vector3 HitNormal;  //���������� ���� ������ �븻���� 
    public Vector3 LastSpotPoint; //���������� �߰ߵ� ������ ����ΰ�?
    public bool IsArrived; //�������� �����ߴ°�?
    public bool IsAttacking; //���� ������ �������ΰ�?

    [field: SerializeField]
    public bool IsHit { get; set; } //���� �°��ִ�?

    public void Init()
    {
        TargetSpotted = false;
        IsArrived = false;
        IsAttacking = false;
        IsHit = false;
    }
}
