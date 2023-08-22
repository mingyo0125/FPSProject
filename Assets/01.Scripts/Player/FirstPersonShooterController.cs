using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShooterController : MonoBehaviour, IDamageAble
{
    [Header("Assigns")]
    [SerializeField]
    private CinemachineVirtualCamera _aimCam;
    [SerializeField]
    private LayerMask _interactionlayerMask;
    [SerializeField]
    private LayerMask _shootlayerMask;

    [Space]

    [Header("GunOffset")]
    [SerializeField]
    private Vector3 _positionOffset = new Vector3(0.15f, -0.18f, 0.3f);
    [SerializeField]
    private Vector3 _rotationOffset = new Vector3(5f, 180f, 0f);

    [Space]

    [Header("Values")]
    [SerializeField]
    private float aimAnimationTime = 10;

    private StarterAssetsInputs _input;

    private Animator _animator;

    Vector2 _screenCenterPoint;

    public Weapon _curWeapon = null;

    Ray _cameraCenterRay;

    private float mapHp = 30;
    private float currentHp;

    [SerializeField]
    private GameObject _testbullet;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();

        _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private void Update()
    {
        //if (_curWeapon != null) { _input.Aim = true; }
        Aim();
        Walk();
    }

    private void Walk()
    {
        if (_curWeapon != null)
        {
            if (_input.move != Vector2.zero)
            {
                if (_input.Aim)
                {
                    SetLayerWeight(_curWeapon.WeaponAnimator, 3, 1f);
                }
                else
                {
                    SetLayerWeight(_curWeapon.WeaponAnimator, 1, 1f);
                }
            }
            else if (_input.move == Vector2.zero)
            {
                if (_input.Aim)
                {
                    SetLayerWeight(_curWeapon.WeaponAnimator, 3, 0f);
                }
                else
                {
                    SetLayerWeight(_curWeapon.WeaponAnimator, 1, 0f);
                }
            }
        }
    }

    private void Start()
    {
        StartCoroutine(Interaction());
        StartCoroutine(Shoot());

        currentHp = mapHp;
    }

    private RaycastHit CameraCenterRayHit(LayerMask layerMask)
    {
        RaycastHit hit;
        _cameraCenterRay = Camera.main.ScreenPointToRay(_screenCenterPoint);

        if (Physics.Raycast(_cameraCenterRay, out hit, 999f, layerMask))
        {
            _testbullet.transform.position = hit.point;
        }
        return hit;
    }

    private void Aim()
    {
        if (_input.Aim && _curWeapon != null)
        {
            _aimCam.gameObject.SetActive(true);

            SetLayerWeight(_animator, 2, 1f);
            SetLayerWeight(_curWeapon.WeaponAnimator, 2, 1f);
        }
        else
        {
            _aimCam.gameObject.SetActive(false);
            SetLayerWeight(_animator, 2, 0f);
            if (_curWeapon != null)
            {
                SetLayerWeight(_curWeapon.WeaponAnimator, 3, 0f);
            }

            if (_curWeapon != null)
            {
                SetLayerWeight(_curWeapon.WeaponAnimator, 2, 0f);
            }
        }
    }

    public IEnumerator Interaction()
    {
        Outline hitOutline = null;
        while (true)
        {
            RaycastHit hit = CameraCenterRayHit(_interactionlayerMask);
            if(hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out hitOutline) && _curWeapon == null)
                {
                    hitOutline.enabled = true;
                }

                if (_input.Interaction && hit.collider.TryGetComponent(out EquipableObject curItem))
                {
                    if (curItem.TryGetComponent(out _curWeapon))
                    {
                        curItem._text.gameObject.SetActive(false);

                        _curWeapon.SetUp(_rotationOffset, _positionOffset);

                    }
                }
            }

            if (hit.collider == null && hitOutline != null)
            {
                hitOutline.enabled = false;
                hitOutline = null;
            }

            yield return null;
        }
    }

    private IEnumerator Shoot()
    {
        while(true)
        {
            RaycastHit hit = CameraCenterRayHit(_shootlayerMask);

            if (_input.Shoot && _curWeapon != null)
            {
                if (hit.collider != null)
                {
                    _curWeapon.SpawnEffect();

                    if (hit.transform.Find("EffectPosition"))
                    {
                        ParticleSystem particle = hit.transform.Find("EffectPosition").GetComponent<ParticleSystem>();
                        
                        particle.transform.position = hit.point;
                        particle.Play();
                    }

                    if(hit.transform.parent.parent.TryGetComponent(out EnemyController enemyController))
                    {
                        enemyController.OnDamage(_curWeapon.Damage);
                    }

                    _input.Shoot = false;
                }
            }
            yield return null;
        }
    }

    private void SetLayerWeight(Animator animtor, int layeridx, float weight)
    {
        animtor.SetLayerWeight(layeridx, Mathf.Lerp(animtor.GetLayerWeight(layeridx), weight, Time.deltaTime * aimAnimationTime));
    }

    public void OnDamage(float damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            Debug.Log("Ã³µðÁü");
        }
    }
}
