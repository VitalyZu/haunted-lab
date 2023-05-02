using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    [SerializeField] private SpawnComponent _bulletSpawer;
    [SerializeField] private SpawnComponent _casingSpawer;
    [SerializeField] private SpawnComponent _grenadeSpawner;
    [SerializeField] private CheckCircleOverlap _intercationCheck;

    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();

        _gameSession.Data.Inventory.OnChange += OnInventoryChange;
    }


    public void BulletSpawn()
    {
        _bulletSpawer.Spawn();
        _casingSpawer.Spawn();
    }

    public void AddInInventory(string id, int count)
    {
        _gameSession.Data.Inventory.AddItem(id, count);
    }
    private void OnInventoryChange(string arg0, int arg1)
    {
        Debug.Log("CHANGED");
    }

    public void Interact()
    {
        _intercationCheck.Check();
    }

    public void Throw()
    {
        var grenadeCount = _gameSession.Data.Inventory.Count("grenade");
        if (grenadeCount > 0)
        {
            _gameSession.Data.Inventory.RemoveItem("grenade", 1);
            _grenadeSpawner.Spawn();
        }
    }

    public void OnHealthChanged(int currentHealth)
    {
        _gameSession.Data.HP.Value = currentHealth;
    }

    private void OnDestroy()
    {
        _gameSession.Data.Inventory.OnChange -= OnInventoryChange;
    }
}
