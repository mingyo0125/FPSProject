using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple UIManager is running");
        }
        Instance = this;
    }

    public void ShowText(TextMeshPro text)
    {
        float moveVec = Mathf.Sin(Time.time) * 0.001f;
        float alpha = Mathf.Sin(Time.time * 5) * 0.4f + 0.6f;

        text.alpha = alpha;
        text.rectTransform.anchoredPosition += new Vector2(0, moveVec * 0.5f);

        text.rectTransform.LookAt(GameManager.Instance.PlayerTrm);
        text.rectTransform.rotation *= Quaternion.Euler(new Vector3(0, 180f, 0));
    }
}
