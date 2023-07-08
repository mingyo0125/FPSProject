using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingShakingCam : MonoBehaviour
{
    [SerializeField]
    private float intensity = 0.1f;
    [SerializeField]
    private float speed = 3f;

    private Vector3 originalPosition;

    private void OnEnable()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        Vector3 offset = new Vector3(
            intensity * Mathf.Sin(Time.time * speed),
            intensity * Mathf.Sin(Time.time * speed * 1.37f),
            intensity * Mathf.Sin(Time.time * speed * 0.73f)
        );

        transform.position = originalPosition + offset;
    }
}
