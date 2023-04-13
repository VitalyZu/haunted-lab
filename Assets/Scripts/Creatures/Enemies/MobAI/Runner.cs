using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] GameObject _target;

    private Creature _creature;

    private void Awake()
    {
        _creature = GetComponent<Creature>();
    }

    private void Start()
    {
        StartCoroutine(Run()); 
    }

    public IEnumerator Run()
    {
        while (enabled)
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            _creature.SetDirection(direction.normalized);

            yield return null;
        }
    }
}
