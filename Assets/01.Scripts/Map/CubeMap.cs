using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    [SerializeField]
    CubeState _cubeState;

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

    public void Set()
    {
        UpdateMap(_cubeState.TrmDic["Front"].ToList(), _front);
        //UpdateMap(_cubeState.TrmDic["Back"], _back);
        //UpdateMap(_cubeState.TrmDic["Up"], _up);
        //UpdateMap(_cubeState.TrmDic["Down"], _down);
        //UpdateMap(_cubeState.TrmDic["Left"], _left);
        //UpdateMap(_cubeState.TrmDic["Right"], _right);
    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        Image[] mapImages = side.GetComponentsInChildren<Image>();

        for (int i = 0; i < face.Count && i < mapImages.Length; i++)
        {
            switch (face[0].name)
            {
                case "Front":
                    for(int j = 0; j < mapImages.Length; j++)
                    {
                        mapImages[j].color = new Color(1, 0.5f, 0, 1);
                    }
                    break;
                    // case "Back":
                    //     mapImages[i].color = Color.red;
                    //     break;
                    // case "Up":
                    //     mapImages[i].color = Color.yellow;
                    //     break;
                    // case "Down":
                    //     mapImages[i].color = Color.white;
                    //     break;
                    // case "Left":
                    //     mapImages[i].color = Color.green;
                    //     break;
                    // case "Right":
                    //     mapImages[i].color = Color.blue;
                    //     break;
            }
        }
    }
}
