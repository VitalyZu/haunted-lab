using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedPartsComponent : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _forceDirection;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _sprite;

    private Rigidbody2D Rigidbody;
    private SpriteRenderer SpriteRender;
    private int Direction;

    public void Start()
    {
        Direction = transform.lossyScale.x > 0 ? -1 : 1;
        
        float color = Random.Range(0.8f, 1f);
        _sprite.color = new Color(color, color, color);
        
        var force = _forceDirection * _speed;
        
        _rb.AddForce(new Vector2((force.x + Random.Range(0f, 0.05f)) * Direction, force.y + Random.Range(0f, 0.05f)), ForceMode2D.Impulse);
    }
}
