using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    [SerializeField]
    private Transform _left;
    [SerializeField]
    private Transform _right;
    [SerializeField]
    private Transform _front;
    [SerializeField]
    private Transform _back;
    [SerializeField]
    private Transform _up;
    [SerializeField]
    private Transform _down;

    [SerializeField]
    private LayerMask _layerMask;

    CubeState _cubeState;

    private void Awake()
    {
        _cubeState = GetComponent<CubeState>();
    }

    private void Start()
    {
        StartCoroutine(CubeHitCorou());
    }

    private IEnumerator CubeHitCorou()
    {
        List<GameObject> hitObjList = new List<GameObject>();
        RaycastHit hit;

        while(true)
        {
            Vector3 ray = _front.transform.position; 

            if(Physics.Raycast(ray, _front.right, out hit, Mathf.Infinity, _layerMask))
            {
                Debug.Log("1");
                hitObjList.Add(hit.collider.gameObject);
            }

                //_cubeState.TrmDic["Front"] = hitObjList;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
