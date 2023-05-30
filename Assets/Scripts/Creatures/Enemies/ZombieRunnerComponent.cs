using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRunnerComponent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _speed;

    [Header("Check")]
    [SerializeField] private LayerMask _groundMask;
    
    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;
    private RaycastHit2D hit;

    private Vector2 _direction;
    private static GameObject _mainTarget;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();

        if(_mainTarget == null) _mainTarget = FindObjectOfType<MainTarget>().gameObject;

        _direction = _mainTarget.transform.position - transform.position;
        _direction.y = 0;
        _direction = _direction.normalized;
    }
    private void Start()
    {
        //StartCoroutine(Move());
    }

    private void FixedUpdate()
    {
        float yVelocity = 0;
        if (!CheckGround())
        {
            yVelocity -= 5f;
        }
        float speed = _direction.x * _speed;

        if (CheckObstacle())
        {
            speed = 0;
            yVelocity = 1f;
        }

        _rb.velocity = new Vector2(speed, yVelocity);
    }

    private bool CheckGround()
    {
        hit = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y + 0.01f, _groundMask);
        return hit.collider != null;
    }

    private bool CheckObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, Vector2.right * Mathf.Sign(transform.localScale.x), _collider.bounds.extents.x + 0.1f, _groundMask);
        return hit.collider != null;
    }

    private IEnumerator Move()
    { 
        yield return null;
    }
}
