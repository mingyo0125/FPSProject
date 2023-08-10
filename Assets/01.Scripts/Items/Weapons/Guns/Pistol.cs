using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] WeaponsSO _weaponsDataSO;

    public override void GetWeapon()
    {
    }

    protected override void SetUp()
    {
        this.damage = _weaponsDataSO.List[0].Damage;
        this.armorCapacity = _weaponsDataSO.List[0].ArmorCapacity;
        this.reloadTime = _weaponsDataSO.List[0].ArmorCapacity;
    }

    private void Awake()
    {
        SetUp();
    }

    private void Update()
    {
        
    }
}
