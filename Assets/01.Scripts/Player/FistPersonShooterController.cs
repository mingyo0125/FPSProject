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

    [SerializeField]
    private float aimAnimationTime = 10;

    private StarterAssetsInputs _input;

    private Animator _animator;


    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //_input.Aim = true;
        Aim();
    }

    private void Aim()
    {
        if(_input.Aim)
        {
            _aimCam.gameObject.SetActive(true);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * aimAnimationTime));
        }
        else
        {
            _aimCam.gameObject.SetActive(false);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * aimAnimationTime));
        }
    }
}
