using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] WeaponsSO _weaponsDataSO;

    public override void GetWeapon()
    {
    }

    public override void SetUp(Vector3 rotateOffset, Vector3 positionOffset)
    {
        this.Damage = _weaponsDataSO.List[0].Damage;
        this.armorCapacity = _weaponsDataSO.List[0].ArmorCapacity;
        this.reloadTime = _weaponsDataSO.List[0].ArmorCapacity;

    }

    public override void SpawnEffect()
    {
    }

    private void Update()
    {
        
    }
}
