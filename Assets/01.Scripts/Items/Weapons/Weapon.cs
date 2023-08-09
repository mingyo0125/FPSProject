using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class Weapon : MonoBehaviour
{
    protected float damage;
    protected float armorCapacity;
    protected float reloadTime;

    protected TextMeshPro _text;
    [HideInInspector]
    public Animator _animator;

    protected abstract void SetUp();

}
