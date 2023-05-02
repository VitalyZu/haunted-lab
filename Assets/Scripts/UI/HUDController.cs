using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private ProgressBar _bar;
    
    private IDisposable _healthCallback;
    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();

        _healthCallback = _gameSession.Data.HP.Subscribe(OnHealthChanged);
        OnHealthChanged(0, _gameSession.Data.HP.Value);
    }

    public void OnHealthChanged(int oldValue, int newValue)
    {
        var max = DefFacade.I.Player.MaxHealth;
        var value = (float)newValue / max;
        _bar.SetBar(value);
    }

    private void OnDestroy()
    {
        _healthCallback.Dispose();
    }
}
