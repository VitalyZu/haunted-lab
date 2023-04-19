using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieSpawnerManager : MonoBehaviour
{
    [SerializeField] private ZombieWaveComponent[] _waves;
    private int _currentWaveIndex = 0;
    private int _layerOrder = 1;

    public int LayerOrder {get { return _layerOrder; } set { _layerOrder = value; } }

    private void Start()
    {
        Array.Sort(_waves);
    }

    private void Update()
    {
        var time = Mathf.Floor(Time.timeSinceLevelLoad);
        if (_currentWaveIndex < _waves.Length && time >= _waves[_currentWaveIndex].StartTime)
        {
            _waves[_currentWaveIndex].Spawn();
            _currentWaveIndex++;
        }
    }
}

