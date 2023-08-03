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
    private LayerMask _interactionlayerMask;

    [SerializeField]
    private float aimAnimationTime = 10;



    private StarterAssetsInputs _input;

    private Animator _animator;

    Vector2 _screenCenterPoint;


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
        Interaction();
    }

    private void Aim()
    {
        if(_input.Aim)
        {
            _aimCam.gameObject.SetActive(true);
            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 1f, Time.deltaTime * aimAnimationTime));
        }
        else
        {
            _aimCam.gameObject.SetActive(false);
            _animator.SetLayerWeight(2, Mathf.Lerp(_animator.GetLayerWeight(2), 0f, Time.deltaTime * aimAnimationTime));
        }
    }

    public void Interaction()
    {
        if(_input.Interaction)
        {
            Debug.Log("321");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(_screenCenterPoint);

            if(Physics.Raycast(ray, out hit, 3f, _interactionlayerMask))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
