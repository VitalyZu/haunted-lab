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
    private List<Collider2D> _interactResult = new List<Collider2D>();

    public float Radius { get { return _radius; } set { _radius = value; } }

    public void Check()
    {
        var cf = new ContactFilter2D();
        cf.SetLayerMask(_mask);

        Physics2D.OverlapCircle(
            transform.position,
            _radius,
            //cf,
             new ContactFilter2D().NoFilter(),
            _interactResult);

        foreach (var item in _interactResult)
        {
            bool isCompare = false;
            foreach (var tag in _tags)
            {
                if (item.gameObject.CompareTag(tag))
                {
                    isCompare = true;
                }
            }

            if (isCompare) _onOverlap?.Invoke(item.gameObject);
        }

        /*for (int i = 0; i < hit; i++)
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
        }*/
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
