using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform _leftDoor, _rightDoor;

    public IEnumerator Start()
    {
        _leftDoor.DOLocalMoveX(1.5f, 0.5f);
        _rightDoor.DOLocalMoveX(-1.5f, 0.5f);

        yield return new WaitForSeconds(10f);

        _leftDoor.DOLocalMoveX(0f, 0.5f);
        _rightDoor.DOLocalMoveX(0f, 0.5f);
    }
}
