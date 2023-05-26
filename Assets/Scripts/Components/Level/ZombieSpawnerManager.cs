using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieSpawnerManager : MonoBehaviour
{
    [SerializeField] private ZombieSpawnerComponent[] _points;
    [SerializeField] private ZombieWaveComponent[] _waves;

    private int _currentWaveIndex = 0;
    private int _layerOrder = 1;
    private int _enemiesCount = 0;

    public ZombieSpawnerComponent[] Points => _points;
    public int LayerOrder {get { return _layerOrder; } set { _layerOrder = value; } }

    private void Start()
    {
        Array.Sort(_waves);

        _enemiesCount = FindObjectsOfType<ZombieComponent>().Length;
        foreach (var wave in _waves)
        {
            _enemiesCount += wave.Number;
        }

        HealthComponent.OnDie += OnScoreChanged;
    }

    private void Update()
    {
        var time = Mathf.Floor(Time.timeSinceLevelLoad);
        if (_currentWaveIndex < _waves.Length && time >= _waves[_currentWaveIndex].StartTime)
        {
            _waves[_currentWaveIndex].Spawn(this);
            _currentWaveIndex++;
        }
        if (_enemiesCount <= 0)
        { Debug.Log("END GAME"); }
    }

    private void OnScoreChanged(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            _enemiesCount--;
        }
    }

    private void OnDestroy()
    {
        HealthComponent.OnDie -= OnScoreChanged;
    }
}

