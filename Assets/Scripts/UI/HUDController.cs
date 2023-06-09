using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private ProgressBar _bar;
    [SerializeField] private List<ItemElement> _items;
    [SerializeField] private Text _score;


    private int _scoreValue = 0;
    private IDisposable _healthCallback;
    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();

        _healthCallback = _gameSession.Data.HP.Subscribe(OnHealthChanged);
        OnHealthChanged(0, _gameSession.Data.HP.Value);

        _gameSession.Data.Inventory.OnChange += OnInventoryChanged;
        InitItems();

        HealthComponent.OnDie += OnScoreChanged;
    }

    private void OnHealthChanged(int oldValue, int newValue)
    {
        var max = DefFacade.I.Player.MaxHealth;
        var value = (float)newValue / max;
        _bar.SetBar(value);
    }

    private void OnInventoryChanged(string id, int value)
    {
        foreach (var item in _items)
        {
            if (item.Id == id)
            { 
                item.SetValue(value);
                return;
            }
        }
    }

    private void OnScoreChanged(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            SetScore();
        }
    }

    private void SetScore()
    {
        //var current = int.Parse(_score.text);
        //current++;
        //_score.text = current.ToString();
        _scoreValue++;
        _score.text = _scoreValue.ToString();

        var currentScore = PlayerPrefs.GetInt("score");
        currentScore++;
        PlayerPrefs.SetInt("score", currentScore);
    }

    private void InitItems()
    {
        var inventory = _gameSession.Data.Inventory;
        foreach (var item in _items)
        {
            var value = inventory.Count(item.Id);
            item.SetValue(value);
        }
    }

    private void OnDestroy()
    {
        _healthCallback.Dispose();
        _gameSession.Data.Inventory.OnChange -= OnInventoryChanged;
        HealthComponent.OnDie -= OnScoreChanged;
    }
}
