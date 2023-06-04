using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, ICheckObstacle
{
    [SerializeField] private LayerCheck _vision;
    [SerializeField] private LayerMask _visionMask;
    [SerializeField] private LayerMask _prevMask;
    [SerializeField] private float _visionRange;
    [SerializeField] float _agroTime = .5f;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] SpawnComponent _bullet;
    [SerializeField] SpawnComponent _case;
    [Header("Sounds")]
    [SerializeField] private AudioClip _shoot;

    private GameObject _currentTarget;
    private Animator _animator;
    private CapsuleCollider2D _collider;
    private Coroutine _coroutine;
    private AudioSource _audio;

    private static readonly int attackKey = Animator.StringToHash("attack");

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_currentTarget == null)
        {
            RaycastHit2D hit = RaycastVision(Vector2.right);
            if (hit.collider != null && !CheckObstacles(hit.collider.gameObject))
            {
                OnEnterVision(hit.collider.gameObject);
                return;
            }

            hit = RaycastVision(Vector2.left);
            if (hit.collider != null && !CheckObstacles(hit.collider.gameObject))
            {
                OnEnterVision(hit.collider.gameObject);
                return;
            }
        }
    }

    private RaycastHit2D RaycastVision(Vector2 _direction)
    {
        return Physics2D.Raycast(_collider.bounds.center, _direction/* * Mathf.Sign(transform.localScale.x)*/, _collider.bounds.extents.x + _visionRange, _visionMask);
    }

    public void OnEnterVision(GameObject target)
    {
        if (!enabled) return;

        //var obstacle = CheckObstacles(target);
        //if (obstacle) return;

        if (_currentTarget == null)
        {
            _currentTarget = target;
            SetDirection();
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Attack());
        }
    }

    public void OnExitVision()
    {
        _currentTarget = null;
    }

    public bool CheckObstacles(GameObject target)
    {
        /*
        var direction = target.transform.position - transform.position;
        RaycastHit2D[] result = new RaycastHit2D[1];
        //Physics2D.RaycastNonAlloc(transform.position, Vector2.right * Mathf.Sign(direction.x), result, 5f, _prevMask);
        Physics2D.RaycastNonAlloc(target.transform.position, transform.position, result, Mathf.Abs(target.transform.position.x - transform.position.x) , _prevMask);
        return result[0].collider != null;
        */
        Vector2 direction = target.transform.position - transform.position;
        direction = direction.normalized;

        var v = direction;//new Vector2(Vector2.Dot(target.transform.position.normalized, direction.normalized), 0);
        var hit = Physics2D.Raycast(_collider.bounds.center, v, Mathf.Abs(target.transform.position.x - transform.position.x), _prevMask);
        Debug.DrawRay(_collider.bounds.center, v);
        return hit.collider != null;
    }

    private void SetDirection()
    {
        var direction = _currentTarget.transform.position - transform.position;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_agroTime);

        if (_currentTarget == null) yield break;

        Vector2 direction = _currentTarget.transform.position - transform.position;
        direction = direction.normalized;
        while (_currentTarget != null)
        {
            var v = direction;//new Vector2(Vector2.Dot(_currentTarget.transform.position.normalized, direction.normalized), 0);
            var hit = Physics2D.Raycast(_collider.bounds.center, v, _collider.bounds.extents.x + _visionRange, _visionMask + _prevMask);
            if (hit.collider == null || !hit.collider.gameObject.CompareTag("Enemy")) break;
            SetDirection();
           _animator.SetTrigger(attackKey);
            yield return new WaitForSeconds(_attackCooldown);
        }
        OnExitVision();
        yield return null;
    }

    public void SpawnBullet()
    {
        _audio.PlayOneShot(_shoot);
        _bullet.Spawn();
        _case.Spawn();
    }

    public void StopAllRoute()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}

public interface ICheckObstacle
{
    public bool CheckObstacles(GameObject target);
}
