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
    }

    private void Start()
    {
        StartCoroutine(Interaction());
    }

    private void Aim()
    {
        if(_input.Aim)
        {
            Debug.Log("321");
            _aimCam.gameObject.SetActive(true);
            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 1f, Time.deltaTime * aimAnimationTime));
        }
        else
        {
            _aimCam.gameObject.SetActive(false);
            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 0f, Time.deltaTime * aimAnimationTime));
        }
    }

    public IEnumerator Interaction()
    {
        RaycastHit hit;
        Outline hitOutline = null;

        while(true)
        {
            Ray ray = Camera.main.ScreenPointToRay(_screenCenterPoint);

            if (Physics.Raycast(ray, out hit, 3f, _interactionlayerMask))
            {
                if (hit.collider.TryGetComponent(out hitOutline))
                {
                    hitOutline.enabled = true;
                }

                if (_input.Interaction)
                {
                    if (hit.collider.transform.parent.GetComponent<EquipableObject>() != null)
                    {
                        hit.collider.transform.parent.SetParent(transform);
                        hit.collider.transform.parent.Find("EquipText").gameObject.SetActive(false);
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
