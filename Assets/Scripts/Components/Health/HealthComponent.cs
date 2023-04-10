using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private UnityEvent _onHit;
    [SerializeField] private OnChangeHealthEvent _onChange;
    [SerializeField] private UnityEvent _onDie;

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
        }
        if (deltaHP < 0)
        {
            _onHit?.Invoke();
        }
        _onChange?.Invoke(deltaHP);
    }
}

public class OnChangeHealthEvent : UnityEvent<int>
{ }
