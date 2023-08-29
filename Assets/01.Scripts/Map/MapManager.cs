using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private List<GameObject> _leftDoor = new List<GameObject>();
    private List<GameObject> _rightDoor = new List<GameObject>();

    private void Awake()
    {
        
    }
}
