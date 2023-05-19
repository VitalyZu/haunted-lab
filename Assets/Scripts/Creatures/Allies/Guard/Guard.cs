using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;
    [SerializeField] private LayerMask _prevMask;
    [SerializeField] float _agroTime = .5f;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] SpawnComponent _bullet;
    [SerializeField] SpawnComponent _case;
    [Header("Sounds")]
    [SerializeField] private AudioClip _shoot;

    private GameObject _currentTarget;
    private Animator _animator;
    private Coroutine _coroutine;
    private AudioSource _audio;

    private static readonly int attackKey = Animator.StringToHash("attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void OnEnterVision(GameObject target)
    {
        if (!enabled) return;

        var obstacle = CheckObstacles(target);
        if (obstacle) return;

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

    private bool CheckObstacles(GameObject target)
    {
        var direction = target.transform.position - transform.position;
        RaycastHit2D[] result = new RaycastHit2D[1];
        Physics2D.RaycastNonAlloc(transform.position, Vector2.right * Mathf.Sign(direction.x), result, 5f, _prevMask);
        return result[0].collider != null;
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
        yield return new WaitForSeconds(_attackCooldown);
        while (_currentTarget != null)
        {
            SetDirection();
           _animator.SetTrigger(attackKey);
            yield return new WaitForSeconds(_attackCooldown);
        } 
    }

    public void SpawnBullet()
    {
        _audio.PlayOneShot(_shoot);
        _bullet.Spawn();
        _case.Spawn();
    }
}
