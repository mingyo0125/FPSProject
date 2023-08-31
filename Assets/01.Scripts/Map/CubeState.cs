using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public Dictionary<string, List<GameObject>> TrmDic = new Dictionary<string, List<GameObject>>();

    private void Start()
    {
        TrmDic.Add("Front", new List<GameObject>());
        TrmDic.Add("Back", new List<GameObject>());
        TrmDic.Add("Up", new List<GameObject>());
        TrmDic.Add("Down", new List<GameObject>());
        TrmDic.Add("Left", new List<GameObject>());
        TrmDic.Add("Right", new List<GameObject>());

    }

}
