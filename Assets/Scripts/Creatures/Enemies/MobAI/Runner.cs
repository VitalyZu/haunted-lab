using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    //[SerializeField] GameObject _target;
    [SerializeField] LayerCheck _attackRange;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private EnemyTreasure[] _treasures;

    private Creature _creature;
    private IEnumerator _routine;
    private static GameObject _target;
    private BoxCollider2D _targetCollider;

    private void Awake()
    {
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
        
        if (_routine != null) StopCoroutine(_routine);

        _routine = state;
        StartCoroutine(state);
    }

    private IEnumerator Run()
    {
        while (enabled)
        {
            if (_attackRange.IsTouchingLayer)
            {
                //yield return null;
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
                    _creature.SetDirection(direction.normalized);
                }
                else 
                {
                    _creature.SetDirection(Vector2.zero);
                }
            }          
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        while (_attackRange.IsTouchingLayer)
        {
            _creature.Attack();
            if (_attackRange.IsTouchingLayer)
            {
                yield return new WaitForSeconds(_attackCooldown);
            }
        }

        StartState(Run());
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
