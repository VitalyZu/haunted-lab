using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    [SerializeField] private SpawnComponent _bulletSpawer;
    [SerializeField] private SpawnComponent _casingSpawer;

    public void BulletSpawn()
    {
        _bulletSpawer.Spawn();
        _casingSpawer.Spawn();
    }
}
