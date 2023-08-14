using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShooterController : MonoBehaviour
{
    [Header("Assigns")]
    [SerializeField]
    private CinemachineVirtualCamera _aimCam;
    [SerializeField]
    private LayerMask _interactionlayerMask;

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

    Weapon _curWeapon = null;

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

        if (_curWeapon != null)
        {
            if (_input.move != Vector2.zero)
            {
                if(_input.Aim)
                {
                    _curWeapon.WeaponAnimator.SetLayerWeight(3, Mathf.Lerp(_curWeapon.WeaponAnimator.GetLayerWeight(3), 1f, Time.deltaTime * aimAnimationTime));
                }
                else
                {
                    _curWeapon.WeaponAnimator.SetLayerWeight(1, Mathf.Lerp(_curWeapon.WeaponAnimator.GetLayerWeight(1), 1f, Time.deltaTime * aimAnimationTime));
                }
            }
            else if (_input.move == Vector2.zero)
            {
                if (_input.Aim)
                {
                    _curWeapon.WeaponAnimator.SetLayerWeight(3, Mathf.Lerp(_curWeapon.WeaponAnimator.GetLayerWeight(3), 0f, Time.deltaTime * aimAnimationTime));
                }
                else
                {
                    _curWeapon.WeaponAnimator.SetLayerWeight(1, Mathf.Lerp(_curWeapon.WeaponAnimator.GetLayerWeight(1), 0f, Time.deltaTime * aimAnimationTime));
                }
            }
        }
    }

    private void Start()
    {
        StartCoroutine(Interaction());
    }

    private void Aim()
    {
        if (_input.Aim && _curWeapon != null)
        {
            _aimCam.gameObject.SetActive(true);

            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 1f, Time.deltaTime * aimAnimationTime));

            _curWeapon.WeaponAnimator.SetLayerWeight(2, Mathf.Lerp(_curWeapon.WeaponAnimator.GetLayerWeight(2), 1f, Time.deltaTime * aimAnimationTime));

        }
        else
        {
            _aimCam.gameObject.SetActive(false);
            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 0f, Time.deltaTime * aimAnimationTime));
            if (_curWeapon != null)
            {
                _curWeapon.WeaponAnimator.SetLayerWeight(3, Mathf.Lerp(_curWeapon.WeaponAnimator.GetLayerWeight(3), 0f, Time.deltaTime * aimAnimationTime));
            }

            if (_curWeapon != null)
            {
                _curWeapon.WeaponAnimator.SetLayerWeight(2, Mathf.Lerp(_curWeapon.WeaponAnimator.GetLayerWeight(2), 0f, Time.deltaTime * aimAnimationTime));
            }
        }
    }

    public IEnumerator Interaction()
    {
        RaycastHit hit;
        Outline hitOutline = null;

        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(_screenCenterPoint);

            if (Physics.Raycast(ray, out hit, 3f, _interactionlayerMask))
            {
                if (hit.collider.TryGetComponent(out hitOutline))
                {
                    hitOutline.enabled = true;
                }

                if (_input.Interaction && hit.collider.TryGetComponent(out EquipableObject curItem))
                {
                    if (curItem.TryGetComponent(out _curWeapon))
                    {
                        curItem._text.gameObject.SetActive(false);

                        _curWeapon.transform.SetParent(Camera.main.transform.Find("Weapon"));
                        _curWeapon.transform.localRotation = Quaternion.Euler(_rotationOffset);
                        _curWeapon.transform.localPosition = _positionOffset;

                        //_curWeapon.transform.Find("arm").transform.localPosition = Vector3.zero;

                        _curWeapon.GetWeapon();

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
}
