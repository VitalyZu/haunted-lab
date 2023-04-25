using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayTriggerComponent : MonoBehaviour
{
    [SerializeField] private string[] _tags;
    [SerializeField] private OnTriggerEvent _onStay;

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var tag in _tags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Debug.Log("Stay trigger " + collision.gameObject.name);
                _onStay?.Invoke(collision.gameObject);
                break;
            }
        }
    }
}
