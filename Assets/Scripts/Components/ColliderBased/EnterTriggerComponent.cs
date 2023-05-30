using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string[] _tags;
    [SerializeField] private bool _isSingleTrigger;
    [Space]
    [SerializeField] private OnTriggerEvent _onEnter;
    [SerializeField] private OnTriggerEvent _onComplete;

    private void Update()
    {
        
    }

    public bool IsSingleTrigger { get => _isSingleTrigger; set => _isSingleTrigger = value; }
    public bool InitCondition { get; set; } = true;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) return;
        foreach (var tag in _tags)
        {
            if (collision.gameObject.CompareTag(tag) && InitCondition)
            {
                _onEnter?.Invoke(collision.gameObject);
                InitCondition = !IsSingleTrigger;
                break;
            }
        }
        _onComplete?.Invoke(collision.gameObject);
    }
}

[Serializable]
public class OnTriggerEvent : UnityEvent<GameObject>
{ 
}
