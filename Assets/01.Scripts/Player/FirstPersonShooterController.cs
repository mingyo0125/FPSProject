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
        //_input.Aim = true;
        Aim();

        if(_input.move != Vector2.zero && _curWeapon != null)
        {
            _curWeapon._animator.SetTrigger("Walk");
        }
    }

    private void Start()
    {
        StartCoroutine(Interaction());
    }

    private void Aim()
    {
        if (_input.Aim)
        {
            _aimCam.gameObject.SetActive(true);

            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 1f, Time.deltaTime * aimAnimationTime));

            if(_curWeapon != null)
            {
                _curWeapon._animator.SetLayerWeight(1, Mathf.Lerp(_curWeapon._animator.GetLayerWeight(1), 1f, Time.deltaTime * aimAnimationTime));
            }

        }
        else
        {
            _aimCam.gameObject.SetActive(false);
            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 0f, Time.deltaTime * aimAnimationTime));

            if (_curWeapon != null)
            {
                _curWeapon._animator.SetLayerWeight(1, Mathf.Lerp(_curWeapon._animator.GetLayerWeight(1), 0f, Time.deltaTime * aimAnimationTime));
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
