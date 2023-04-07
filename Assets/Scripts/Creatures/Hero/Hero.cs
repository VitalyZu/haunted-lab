using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [Header("Check")]
    [SerializeField] private LayerCheck _groundCheck;

    private Vector2 _direction;
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _isGrounded;
    private bool _isJumping;

    private static readonly int runningKey = Animator.StringToHash("running");
    private static readonly int verticalVelocityKey = Animator.StringToHash("vertical-velocity");
    private static readonly int groundKey = Animator.StringToHash("ground");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _isGrounded = _groundCheck.IsTouchingLayer;
        Debug.Log(_isGrounded);
    }

    private void FixedUpdate()
    {
        float xVelocity = _direction.x * _speed;
        float yVelocity = CalculateYVelocity();

        _rb.velocity = new Vector2(xVelocity, yVelocity);

        if(_direction.x != 0) SetSpriteDirection(_direction);

        float velocityForAnimator = _rb.velocity.y;

        //if (_rb.velocity.y > -0.9) velocityForAnimator = 0;

        _animator.SetBool(groundKey, _isGrounded);
        _animator.SetFloat(verticalVelocityKey, velocityForAnimator);
        _animator.SetBool(runningKey, _direction.x != 0);
        
    }

    private float CalculateYVelocity()
    {
        var yVelocity = _rb.velocity.y;

        _isJumping = _direction.y > 0;

        if (_isGrounded && _isJumping)
        {
            bool isFalling = _rb.velocity.y <= .001f;
            yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
        }
        else if (_rb.velocity.y > 0 && !_isJumping)
        {
            yVelocity *= .5f;
        }

        return yVelocity;
    }

    private float CalculateJumpVelocity(float velocity)
    {

        if (_isGrounded)
        {
            velocity += _jumpSpeed;
        }

        return velocity;
    }

    private void SetSpriteDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    internal void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }


}