using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private float _jumpForceMultiplier = 1;
    private Rigidbody _rigidbody;
    private bool _isGrounded = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce * _jumpForceMultiplier);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Road road))
        {
            _isGrounded = true;
        }

        if(collision.gameObject.TryGetComponent(out JumpBooster booster))
        {
            _jumpForceMultiplier = booster.BoostMultiplier;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out JumpBooster booster))
        {
            _jumpForceMultiplier = 1;
        }
    }
}
