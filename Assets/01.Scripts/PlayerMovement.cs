using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{
    [Header("MovementValues")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float dashPower;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCoolTime;

    [Header("ETC")]
    [SerializeField]
    private float checkGroundRayDistance = 0.4f;
    [SerializeField]
    private LayerMask _layerMask;


    Rigidbody _rigidbody;
    Vector3 _dir;


    private bool isGround;
    private bool canDash = true;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");
        _dir.Normalize();

        CheckGround();

        if(Input.GetKeyDown(KeyCode.Space) && isGround)   
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && isGround && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region Movement
    private void Move()
    {
        if (_dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.position.x) != Mathf.Sign(_dir.x) || Mathf.Sign(transform.position.z) != Mathf.Sign(_dir.z))
            {
                transform.Rotate(0, 1, 0);
            }

            transform.forward = Vector3.Lerp(transform.forward, _dir, rotateSpeed * Time.deltaTime);
        }
        _rigidbody.MovePosition(transform.position + _dir * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        Vector3 jumpVec = Vector3.up * jumpPower;
        _rigidbody.AddForce(jumpVec, ForceMode.VelocityChange);
    }

    private IEnumerator Dash()
    {
        canDash = false;

        Vector3 dashVec = transform.forward * dashPower;
        _rigidbody.AddForce(dashVec, ForceMode.VelocityChange);
        Debug.Log("Dash");

        yield return new WaitForSeconds(dashDuration);

        _rigidbody.velocity = Vector3.zero;

        yield return new WaitForSeconds(dashCoolTime);
        canDash = true;
    }
    #endregion

    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, checkGroundRayDistance, _layerMask)) { isGround = true; }
        else { isGround = true; }
    }
}
