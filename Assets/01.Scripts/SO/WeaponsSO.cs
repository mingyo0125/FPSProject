using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaPonPair
{
    public string name;
    public float Damage;
    public float ArmorCapacity;
    public float ReloadTime;
}

[CreateAssetMenu(menuName = "SO/Weapon/Weapons")]
public class WeaponsSO : ScriptableObject
{
    public List<WeaPonPair> List;
}
