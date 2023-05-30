using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    private Collider2D _collider;
    private bool _isTouchingLayer;

    public bool IsTouchingLayer => _isTouchingLayer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if(Time.frameCount % 3 == 0)
            _isTouchingLayer = _collider.IsTouchingLayers(_groundMask);
    }
    private void OnTriggerExit2D(Collider2D collision)

    {
        //if (Time.frameCount % 3 == 0)
            _isTouchingLayer = _collider.IsTouchingLayers(_groundMask);
    }
}
