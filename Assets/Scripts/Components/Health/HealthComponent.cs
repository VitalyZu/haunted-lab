using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private UnityEvent _onHit;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private OnChangeHealthEvent _onChange;

    public static Action<GameObject> OnDie;
    
    private void Awake()
    {
        var heroComponent = GetComponent<Hero>();
        if (heroComponent != null)
        {
            _health = DefFacade.I.Player.MaxHealth;
        }
    }

    public void SetHealth(int deltaHP)
    {
        _health += deltaHP;
        if (_health <= 0)
        {
            _onDie?.Invoke();
            OnDie?.Invoke(gameObject);
        }
        if (deltaHP < 0)
        {
            _onHit?.Invoke();
        }
        _onChange?.Invoke(_health);
    }
}

[Serializable]
public class OnChangeHealthEvent : UnityEvent<int>
{ }

