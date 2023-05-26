using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZombieWaveComponent : IComparable
{
    //[SerializeField] private ZombieSpawnerComponent _point;
    [SerializeField] private int _pointNumber;
    [SerializeField] private bool _randomPoint;
    [SerializeField] private int _number;
    [SerializeField] private float _startTime;
    [SerializeField] private float _enemiesSwpawnDelay;
    [SerializeField] private GameObject[] _types;

    public float StartTime => _startTime;
    public int Number => _number;

    public void Spawn(ZombieSpawnerManager manager)
    {
        if (_pointNumber > manager.Points.Length || _randomPoint)
        {
            _pointNumber = UnityEngine.Random.Range(0, manager.Points.Length);
        }
        manager.Points[_pointNumber].Spawn(_number, _types, _enemiesSwpawnDelay);
    }

    public int CompareTo(object obj)
    {
        if (obj is ZombieWaveComponent wave) return _startTime.CompareTo(wave._startTime);
        return 0;
    }
}
