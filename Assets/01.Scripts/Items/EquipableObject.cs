using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class EquipableObject : MonoBehaviour
{
    [SerializeField]
    public TextMeshPro _text;

    public bool isRange = false;

    public void ShowTextCheck()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.PlayerTrm.position) <= 3f)
        {
            if (isRange == false) { UIManager.Instance.ShowText(_text); return; }

            _text.DOFade(1, 0.1f).OnComplete(() =>
            {
                isRange = false;
                UIManager.Instance.ShowText(_text);
                _text.DOKill();
            });

        }
        else if (Vector3.Distance(transform.position, GameManager.Instance.PlayerTrm.position) >= 3f)
        {
            if(_text.color.a > 0)
            {
                HideText();
            }
        }
    }

    public void HideText()
    {
        _text.DOFade(0, 1f).OnComplete(() =>
        {
            _text.DOKill();
        });
    }

    private void Update()
    {
        ShowTextCheck();
    }
}
