using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _aimCam;

    private StarterAssetsInputs _input;


    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        if(_input.Aim)
        {
            _aimCam.gameObject.SetActive(true);
        }
        else
        {
            _aimCam.gameObject.SetActive(false);
        }
    }
}
