using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayTriggerComponent : MonoBehaviour
{
    [SerializeField] private string[] _tags;
    [SerializeField] private OnTriggerEvent _onStay;
    [SerializeField] private OnTriggerEvent _onExit;
    [SerializeField] private Guard _checkObstacles;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_checkObstacles != null)
        {
            bool check = _checkObstacles.CheckObstacles(collision.gameObject);
            if (check) return;
        }
        foreach (var tag in _tags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                _onStay?.Invoke(collision.gameObject);
                break;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var tag in _tags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                _onExit?.Invoke(collision.gameObject);
                break;
            }
        }
    }
}
