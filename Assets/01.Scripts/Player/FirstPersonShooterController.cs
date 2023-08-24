using Cinemachine;
using DG.Tweening;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    private Vector3 _riflePositionOffset = new Vector3(0.15f, -0.18f, 0.3f);
    [SerializeField]
    private Vector3 _rifleRotationOffset = new Vector3(5f, 180f, 0f);

    [Space]

    [Header("Values")]
    [SerializeField]
    private float aimAnimationTime = 10;
    private float interactionRange = 5f;
    private float shootRange = 50f;

    private StarterAssetsInputs _input;

    private Animator _animator;

    Vector2 _screenCenterPoint;

    public Weapon _curWeapon = null;

    Ray _cameraCenterRay;

    private float mapHp = 30;
    private float currentHp;

    [SerializeField]
    private GameObject _testbullet;

    [SerializeField]
    Image _bloodHubEffect;

    [SerializeField]
    UnityEvent OnDie;

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

    private RaycastHit CameraCenterRayHit(LayerMask layerMask, float distance)
    {
        RaycastHit hit;
        _cameraCenterRay = Camera.main.ScreenPointToRay(_screenCenterPoint);

        if (Physics.Raycast(_cameraCenterRay, out hit,distance , layerMask))
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
        EquipableObject curItem = null;

        while (true)
        {
            RaycastHit hit = CameraCenterRayHit(_interactionlayerMask, interactionRange);
            if(hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out hitOutline) && _curWeapon == null)
                {
                    hitOutline.enabled = true;
                }

                if (_input.Interaction && hit.collider.TryGetComponent(out curItem))
                {
                    if (curItem.TryGetComponent(out _curWeapon))
                    {
                        curItem._text.gameObject.SetActive(false);

                        _curWeapon.SetUp(_rifleRotationOffset, _riflePositionOffset);

                    }
                }
            }

            if (hit.collider == null && hitOutline != null)
            {
                hitOutline.enabled = false;
                hitOutline = null;
            }

            _input.Interaction = false;
            yield return null;
        }
    }

    private IEnumerator Shoot()
    {
        while(true)
        {
            RaycastHit hit = CameraCenterRayHit(_shootlayerMask, shootRange);

            if (_input.Shoot && _curWeapon != null)
            {
                if (hit.collider != null)
                {
                    _curWeapon.SpawnEffect();

                    if (hit.transform.parent.Find("EffectPosition"))
                    {
                        ParticleSystem particle = hit.transform.parent.Find("EffectPosition").GetComponent<ParticleSystem>();
                        
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

        Debug.Log($"Player : {currentHp}");

        _bloodHubEffect.DOKill();
        _bloodHubEffect.DOFade(1, 0.5f).OnComplete(() =>
        {
            _bloodHubEffect.DOFade(0, 0.5f);
        });

        if(currentHp <= 0)
        {
            OnDie?.Invoke();
        }
    }

    public void PlayerDieAnimation()
    {
        Vector3 playerDieRotate = new Vector3(-60, 0, 0);
        transform.DORotateQuaternion(Quaternion.Euler(playerDieRotate), 2f);
    }
}
