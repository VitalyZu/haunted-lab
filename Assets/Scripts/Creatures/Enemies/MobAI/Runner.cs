using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] LayerCheck _attackRange;

    private Creature _creature;
    private IEnumerator _routine;

    private void Awake()
    {
        _creature = GetComponent<Creature>();
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
            yield return null;//new WaitForSeconds(_attackCooldown);
        }

        StartState(Run());
    }
}
