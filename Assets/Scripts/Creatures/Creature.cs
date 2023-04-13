using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [Header("Check")]
    [SerializeField] private LayerCheck _groundCheck;
    [Header("Spawners")]

    protected Rigidbody2D _rb;
    protected Animator _animator;
    private Vector2 _direction;
    protected int _pushDirection;
    private float _pushDuration = 0.2f;
    
    private bool _isAttack = false;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isHit;

    private Coroutine _hitCoroutine;

    private static readonly int runningKey = Animator.StringToHash("running");
    private static readonly int verticalVelocityKey = Animator.StringToHash("vertical-velocity");
    private static readonly int groundKey = Animator.StringToHash("ground");

    private static readonly int fireKey = Animator.StringToHash("fire");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        _isGrounded = _groundCheck.IsTouchingLayer;
    }

    private float CalculateJumpVelocity(float velocity)
    {
        if (_isGrounded)
        {
            velocity += _jumpSpeed;
        }

        return velocity;
    }

    private void FixedUpdate()
    {
        if (!_isAttack)
        {
            float xVelocity = !_isHit ? _direction.x * _speed * Random.Range(0, 2f) : _pushDirection * Random.Range(0, 5f);
            float yVelocity = CalculateYVelocity();

            _rb.velocity = new Vector2(xVelocity, yVelocity);

            if (_direction.x != 0) SetSpriteDirection(_direction);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

        float velocityForAnimator = _rb.velocity.y;

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

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Fire()
    {
        if (!_isAttack && _rb.velocity.y == 0)
        {
            _isAttack = true;
            _animator.SetTrigger(fireKey);
        }
    }
    private void SetFireState()
    {
        _isAttack = false;
    }

    public void PushSelf(int direction)
    {
        _pushDirection = direction;
        _isHit = true;
        //
        _rb.velocity = new Vector2(direction, _rb.velocity.y);
        //
        if(_hitCoroutine != null) StopCoroutine(_hitCoroutine);
        _hitCoroutine = StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(_pushDuration);
        _isHit = false;
        _hitCoroutine = null;
    }
}
