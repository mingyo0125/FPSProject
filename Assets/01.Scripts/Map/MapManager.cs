using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [SerializeField]
    private Transform[] _mapArr;

    private void Awake()
    {
        
    }

}
