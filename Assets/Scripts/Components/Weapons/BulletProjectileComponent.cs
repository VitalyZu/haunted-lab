using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectileComponent : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D Rigidbody;
    private int Direction;

    public void Start()
    {
        Direction = transform.lossyScale.x > 0 ? 1 : -1;
        Rigidbody = GetComponent<Rigidbody2D>();
        var force = new Vector2(_speed * Direction, 0);
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    public void Restart(Transform target)
    {
        Direction = target.lossyScale.x > 0 ? 1 : -1;
        var force = new Vector2(_speed * Direction, 0);
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

}
