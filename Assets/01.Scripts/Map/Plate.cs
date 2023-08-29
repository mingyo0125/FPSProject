using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    [SerializeField]
    List<Material> _materialList = new List<Material>();

    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        int rand = Random.Range(0, 10);
        _meshRenderer.material = _materialList[rand];
    }

}
