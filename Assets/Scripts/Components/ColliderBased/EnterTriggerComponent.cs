using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string[] _tags;
    [SerializeField] private OnTriggerEvent _onEnter;
    [SerializeField] private OnTriggerEvent _onComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in _tags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                _onEnter?.Invoke(collision.gameObject);
                break;
            }
        }
        _onComplete?.Invoke(gameObject);
    }
}

[Serializable]
public class OnTriggerEvent : UnityEvent<GameObject>
{ 
}
