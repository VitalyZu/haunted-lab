using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectileComponent : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private EnterTriggerComponent _triggerComponent;
    private int Direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _triggerComponent = GetComponent<EnterTriggerComponent>();
    }

    public void Start()
    {
        Direction = transform.lossyScale.x > 0 ? 1 : -1;
        
        var force = new Vector2(_speed * Direction, 0);
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void Restart(Transform target)
    {
        _triggerComponent.InitCondition = true;
        _triggerComponent.IsSingleTrigger = true;

        Direction = target.lossyScale.x > 0 ? 1 : -1;
        var force = new Vector2(_speed * Direction, 0);
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

}
