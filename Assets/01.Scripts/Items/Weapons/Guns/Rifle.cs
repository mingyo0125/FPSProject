using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] WeaponsSO _weaponsDataSO;
    [SerializeField] ParticleSystem _particleSystem;

    public override void GetWeapon()
    {
        
    }

    public override void SetUp(Vector3 rotateOffset, Vector3 positionOffset)
    {
        this.Damage = _weaponsDataSO.List[1].Damage;
        this.armorCapacity = _weaponsDataSO.List[1].ArmorCapacity;
        this.reloadTime = _weaponsDataSO.List[1].ArmorCapacity;

        this.gameObject.layer = 9;

        transform.SetParent(Camera.main.transform.Find("Weapon"));
        transform.localRotation = Quaternion.Euler(rotateOffset);
        transform.localPosition = positionOffset;

        transform.Find("Arm").gameObject.SetActive(true);
        _particleSystem.gameObject.SetActive(true);

        WeaponAnimator = GetComponent<Animator>();

    }

    public override void SpawnEffect()
    {
        _particleSystem.Play();
    }

    private void Awake()
    {
        
    }

}
