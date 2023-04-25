using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;
    [SerializeField] float _agroTime = .5f;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] SpawnComponent _bullet;
    [SerializeField] SpawnComponent _case;

    private GameObject _currentTarget;
    private Animator _animator;
    private Coroutine _coroutine;

    private static readonly int attackKey = Animator.StringToHash("attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnEnterVision(GameObject target)
    {
        if (_currentTarget == null)
        {
            _currentTarget = target;
            SetDirection();
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Attack());
        }
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
            _animator.SetTrigger(attackKey);
            yield return new WaitForSeconds(_attackCooldown);
        } 
    }

    public void SpawnBullet()
    {
        _bullet.Spawn();
        _case.Spawn();
    }
}
