using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class EquipableObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text;

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

            });

        }
        else if (Vector3.Distance(transform.position, GameManager.Instance.PlayerTrm.position) >= 3f)
        {
            _text.DOFade(0, 1f).OnComplete(() =>
            {
                isRange = true;
            });
        }
    }

    private void Update()
    {
        ShowTextCheck();
    }
}
