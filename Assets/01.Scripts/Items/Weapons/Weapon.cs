using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class Weapon : MonoBehaviour
{
    protected float damage;
    protected float armorCapacity;
    protected float reloadTime;
    protected bool canEquip = false;

    protected TextMeshPro _text;

    protected abstract void SetUp();

    protected virtual void ShowTextCheck()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.PlayerTrm.position) <= 3f)
        {
            if(canEquip == false) { UIManager.Instance.ShowText(_text); return; }

            _text.DOFade(1, 0.1f).OnComplete(() =>
            {
                canEquip = false;
                UIManager.Instance.ShowText(_text);
            });
        }
        else if(Vector3.Distance(transform.position, GameManager.Instance.PlayerTrm.position) >= 3f)
        {
            _text.DOFade(0, 1f).OnComplete(() =>
            {
                canEquip = true;
            });
        }
    }
}
