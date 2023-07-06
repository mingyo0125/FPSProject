using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField]
    private LayerMask _aimlayerMask;


    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;


    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;
    [SerializeField]
    private float rotateSpeed;


    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
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
        }
        else
        {

        }

        if (_starterAssetsInputs.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);

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
        }

        if(_starterAssetsInputs.shoot)
        {
            //¹Ù²Ù±â

            transform.forward = Vector3.Lerp(transform.forward, mouseWorldPos, Time.deltaTime * rotateSpeed);

            //Shoot
        }
        else
        {

        }

        
    }
}
