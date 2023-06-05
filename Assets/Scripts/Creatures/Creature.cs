using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _damageJumpSpeed;
    [Header("Check")]
    //[SerializeField] private LayerCheck _groundCheck;
    [SerializeField] protected LayerMask _groundMask;
    [Header("Spawners")]

    protected Rigidbody2D _rb;
    protected CapsuleCollider2D _collider;
    protected Animator _animator;
    protected AudioSource _audio;
    protected Vector2 _direction;
    protected int _pushDirection;
    private float _pushDuration = 0.2f;
    
    private bool _isAttack = false;
    protected bool _isGrounded;
    private bool _isJumping;
    private bool _isHit;

    private Coroutine _hitCoroutine;

    protected static readonly int runningKey = Animator.StringToHash("running");
    protected static readonly int verticalVelocityKey = Animator.StringToHash("vertical-velocity");
    protected static readonly int groundKey = Animator.StringToHash("ground");
    private static readonly int attackKey = Animator.StringToHash("attack");
    private static readonly int hitKey = Animator.StringToHash("hit");

    public float Speed { get => _speed; set => _speed = value; }
    public float PushDuration { get => _pushDuration; set => _pushDuration = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y + 0.1f, _groundMask);
        _isGrounded = hit.collider != null;//_groundCheck.IsTouchingLayer;
        //Debug.DrawRay(_collider.bounds.center, Vector2.down * (_collider.bounds.extents.y + 0.1f));
    }

    private float CalculateJumpVelocity(float velocity)
    {
        if (_isGrounded)
        {
            velocity += _jumpSpeed;
        }

        return velocity;
    }

    protected virtual void FixedUpdate()
    {
        if (!_isAttack || _isHit)
        {
            var speed = _direction.x * Speed;
            float xVelocity = !_isHit ? 
                speed : 
                _pushDirection != 0 ? 
                _pushDirection * Random.Range(0, 3f) :
                speed;
            float yVelocity = CalculateYVelocity();

            _rb.velocity = new Vector2(xVelocity, yVelocity);

            if (_direction.x != 0) SetSpriteDirection(_direction);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

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
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);//new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, 1);//new Vector3(-1, 1, 1);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Attack()
    {
        if (!_isAttack && _rb.velocity.y == 0)
        {
            _isAttack = true;
            _animator.SetTrigger(attackKey);
        }
    }
    public void SetFireStateFalse()
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
        yield return new WaitForSeconds(PushDuration);
        _isHit = false;
        _hitCoroutine = null;
    }

    public virtual void GetDamage()
    {
        _isHit = true;
        _rb.velocity = new Vector2(_rb.velocity.x, _damageJumpSpeed);
        _animator.SetTrigger(hitKey);
    }

    public void EndHit()
    {
        _isHit = false;
    }
}
