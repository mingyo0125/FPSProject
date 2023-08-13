using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] WeaponsSO _weaponsDataSO;

    public override void GetWeapon()
    {
        
    }

    protected override void SetUp()
    {
        this.damage = _weaponsDataSO.List[1].Damage;
        this.armorCapacity = _weaponsDataSO.List[1].ArmorCapacity;
        this.reloadTime = _weaponsDataSO.List[1].ArmorCapacity;

        WeaponAnimator = GetComponent<Animator>();
    }

    private void Awake()
    {
        SetUp();
    }
}
