using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class PopUpText : PoolableMono
{
    TextMeshPro _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    public override void Init()
    {
        ShowText();
    }

    public void TextSetUp(float damage)
    {
        Debug.Log("SetpUp");
        _text.SetText(damage.ToString());
        _text.color = damage == 10 ? Color.red : Color.white;
        _text.transform.localPosition = Vector3.zero;
    }

    private void ShowText()
    {
        _text.rectTransform.DOAnchorPosY(3f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    private void Update()
    {
        _text.rectTransform.LookAt(GameManager.Instance.PlayerTrm.position);
        _text.rectTransform.rotation *= Quaternion.Euler(new Vector3(0, 180f, 0));
    }
}
