using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnListComponent : MonoBehaviour
{
    [SerializeField] private SpawnData[] _spawners;

    public void Spawn(string id)
    {
        bool Compare(SpawnData data) => data.Id == id;
        //_spawners.FirstOrDefault(spwn => spwn.Id == id);
        var spawner = _spawners.FirstOrDefault(Compare);
        spawner?.spawnComponent.Spawn();
    }

    [Serializable]
    public class SpawnData
    {
        public string Id;
        public SpawnComponent spawnComponent;
    }
}
