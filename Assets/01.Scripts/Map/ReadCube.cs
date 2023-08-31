using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField]
    CubeMap _cubeMap;

    private void Awake()
    {
        _cubeState = GetComponent<CubeState>();
    }

    private void Update()
    {
        List<GameObject> hitObjList = new List<GameObject>();
        Vector3 ray = _front.transform.position;

        if (Physics.Raycast(ray, -_front.right, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            Debug.DrawRay(ray, -_front.right * hit.distance, Color.yellow);
            hitObjList.Add(hit.collider.gameObject);
            Debug.Log(hit.collider.name);
        }
        else
        {
            Debug.Log("312");
            Debug.DrawRay(ray, -_front.right * 1000, Color.yellow);
        }

        _cubeState.UpdateFrontList("Front", hitObjList);
        _cubeMap.Set();
    }
}
