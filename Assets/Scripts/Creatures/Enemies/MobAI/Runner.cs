using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    //[SerializeField] GameObject _target;
    [SerializeField] LayerCheck _attackRange;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] private EnemyTreasure[] _treasures;

    private Creature _creature;
    private IEnumerator _routine;
    private static GameObject _target;
    private BoxCollider2D _targetCollider;
    private bool _isAttack = false;
    private int id;
    private bool _canSpeedBoost = true;

    private void Awake()
    {
        id = gameObject.GetInstanceID();
        _creature = GetComponent<Creature>();
        
        if (_target == null)
            _target = FindObjectOfType<MainTarget>().gameObject;

        _targetCollider = _target.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartState(Run());
    }

    private void StartState(IEnumerator state)
    {
        _creature.SetDirection(Vector2.zero);

        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }

        _routine = state;
        StartCoroutine(state);
    }

    private IEnumerator Run()
    {
        while (enabled && !_isAttack)
        {
            RaycastHit2D[] hit = new RaycastHit2D[1];
            //Physics2D.Raycast(_collider.bounds.center, Vector2.right * Mathf.Sign(transform.localScale.x), _collider.bounds.extents.x + 0.11f, _attackMask);
            Physics2D.RaycastNonAlloc(_collider.bounds.center, Vector2.right * Mathf.Sign(transform.localScale.x), hit, _collider.bounds.extents.x + 0.11f, _attackMask);
            
            if(hit[0].collider != null)
            //if (hit.collider != null)
            //if (_attackRange.IsTouchingLayer)
            {
                //yield return null;
                _isAttack = true;
                StartState(Attack());
            }
            else
            {
                if (_target != null)
                {
                    if (!_targetCollider.enabled)
                    {
                        _creature.SetDirection(Vector2.zero);
                        this.enabled = false;
                        break;
                    }
                    var direction = _target.transform.position - transform.position;
                    direction.y = 0;

                    var random = UnityEngine.Random.RandomRange(0, 100);
                    if (20 > random && _canSpeedBoost)
                    {
                        _canSpeedBoost = false;
                        StartCoroutine(BoostSpeed());
                    }
                    else if(_canSpeedBoost)
                    {
                        _canSpeedBoost = false;
                        StartCoroutine(RefreshBoostSpeed());

                    }

                    _creature.SetDirection(direction.normalized);
                }
                else 
                {
                    _creature.SetDirection(Vector2.zero);
                }
            }          
            yield return null;
        }
        yield return null;
    }

    private IEnumerator BoostSpeed()
    {
        var speed = _creature.Speed;
        var pushDuration = _creature.PushDuration;
        
        _creature.Speed = 2;
        _creature.PushDuration = 0.05f;

        yield return new WaitForSeconds(3f);

        _creature.Speed = speed;
        _creature.PushDuration = pushDuration;

        StartCoroutine(RefreshBoostSpeed());
    }

    private IEnumerator RefreshBoostSpeed()
    {
        yield return new WaitForSeconds(3f);
        _canSpeedBoost = true;
    }

    private IEnumerator Attack()
    {
        _isAttack = true;
        while (_attackRange.IsTouchingLayer)
        {
            _creature.Attack();
            if (_attackRange.IsTouchingLayer)
            {
                yield return new WaitForSeconds(_attackCooldown);
            }
        }
        _isAttack = false;
        yield return null;
        StartState(Run());
    }

    [ContextMenu("StopAllRoute")]
    public void StopAllRoute()
    {
        if (_routine != null)
        {
            _creature.SetDirection(Vector2.zero);
            StopCoroutine(_routine);
        }
    }

    public void SpawnTreasure()
    {
        foreach (var item in _treasures)
        {
            var random = UnityEngine.Random.RandomRange(0, 100);
            if (item.Probability > random)
            {
                //SpawnUtil.Spawn(item.Prefab, transform.position);
                Pool.Instance.Get(item.Prefab, transform);
                break;
            }
        }
    }

    [Serializable]
    public class EnemyTreasure
    {
        [SerializeField] private GameObject _prefab;
        [Range(0,100)]
        [SerializeField] private float _probability;

        public GameObject Prefab => _prefab;
        public float Probability => _probability;
    }
}
