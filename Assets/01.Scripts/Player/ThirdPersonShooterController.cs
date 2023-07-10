using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;
using UnityEditor.Rendering;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField]
    private LayerMask _aimlayerMask;
    [SerializeField]
    private GameObject test;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;
    private Animator _animator;


    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;
    [SerializeField]
    private float rotateSpeed;

    private Transform hitPoint;


    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Aimming();
    }

    private void Aimming()
    {
        Vector3 mouseWorldPos = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);


        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, _aimlayerMask))
        {
            mouseWorldPos = hitInfo.point;
            test.transform.position = hitInfo.point;
            hitPoint = hitInfo.transform;
        }

        if (_starterAssetsInputs.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);


            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));

            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));


            Vector3 worldTargetPos = mouseWorldPos;
            worldTargetPos.y = transform.position.y;
            Vector3 aimDirection = (worldTargetPos - transform.position).normalized;
             
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * rotateSpeed);

        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);


            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

        }

        if (_starterAssetsInputs.shoot)
        {
            if(hitPoint != null)
            {
                //hit something
                if (hitPoint.gameObject.GetComponent<BulletTartget>() != null)
                {
                    VFX hitVFX = PoolManager.Instance.Pop("VFX_HitGreen") as VFX;
                    hitVFX.transform.position = test.transform.position;
                    hitVFX.transform.rotation = Quaternion.identity;
                }
                else
                {
                    VFX hitVFX = PoolManager.Instance.Pop("VFX_HitRed") as VFX;
                    hitVFX.transform.position = test.transform.position;
                    hitVFX.transform.rotation = Quaternion.identity;
                }
            }
        }
        

        
    }
}
