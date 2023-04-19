using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZombieWaveComponent : IComparable
{
    [SerializeField] private ZombieSpawnerComponent _point;
    [SerializeField] private int _number;
    [SerializeField] private float _startTime;
    [SerializeField] private float _enemiesSwpawnDelay;
    [SerializeField] private GameObject[] _types;

    public float StartTime => _startTime;

    public void Spawn()
    {
        _point.Spawn(_number, _types, _enemiesSwpawnDelay);
    }

    public int CompareTo(object obj)
    {
        if (obj is ZombieWaveComponent wave) return _startTime.CompareTo(wave._startTime);
        return 0;
    }
}
