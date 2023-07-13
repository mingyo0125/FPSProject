using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Entity/Enemy/SpiderDataSO")]
public class SpiderDataSO : ScriptableObject
{
    public int MaxHP;
    public float MoveSpeed;
    public float RotateSpeed;
    public int AtkDamage;
    public float AtkCoolTime; //공격 쿨타임
}
