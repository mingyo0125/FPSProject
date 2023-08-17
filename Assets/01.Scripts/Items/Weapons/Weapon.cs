using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Weapon : MonoBehaviour
{
    protected float damage;
    protected float armorCapacity;
    protected float reloadTime;

    private float intensity = 0.001f;
    
    protected TextMeshPro _text;
    [HideInInspector]
    public Animator WeaponAnimator;

    public abstract void GetWeapon();
    
    public abstract void SetUp(Vector3 rotateOffset, Vector3 positionOffset);

    public abstract void SpawnEffect();

}
