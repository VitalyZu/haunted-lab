using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] float _radius = 1f;
    [SerializeField] OnOverlapEvent _onOverlap;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private string[] _tags;
    private Collider2D[] _interactResult = new Collider2D[10];

    public void Check()
    {
        int hit = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            _radius,
            _interactResult,
            _mask);

        for (int i = 0; i < hit; i++)
        {
            bool isCompare = false;
            foreach (var tag in _tags)
            {
                if (_interactResult[i].gameObject.CompareTag(tag))
                {
                    isCompare = true;
                }
            }

            if (isCompare) _onOverlap?.Invoke(_interactResult[i].gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = new Color(1f, 1f, 1f, 0.45f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }
#endif
}

[Serializable]
public class OnOverlapEvent : UnityEvent<GameObject>
{
}
