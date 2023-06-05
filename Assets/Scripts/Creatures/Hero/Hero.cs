using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    [SerializeField] private SpawnComponent _bulletSpawer;
    [SerializeField] private SpawnComponent _casingSpawer;
    [SerializeField] private SpawnComponent _grenadeSpawner;
    [SerializeField] private SpawnComponent _guardSpawner;
    [SerializeField] private GameObject[] _guardPrefabs;
    [SerializeField] private CheckCircleOverlap _intercationCheck;
    [SerializeField] private LayerCheck _groundCheck;
    [Header("Sounds")]
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _collectItem;

    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();

        _gameSession.Data.Inventory.OnChange += OnInventoryChange;
    }

    private void Update()
    {
        _isGrounded = _groundCheck.IsTouchingLayer;
    }

    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        float velocityForAnimator = _rb.velocity.y;
        _animator.SetBool(groundKey, _isGrounded);
        _animator.SetFloat(verticalVelocityKey, velocityForAnimator);
        
    }

    public void BulletSpawn()
    {
        _audio.PlayOneShot(_shoot);
        _bulletSpawer.Spawn();
        _casingSpawer.Spawn();
    }

    public void AddInInventory(string id, int count)
    {
        _gameSession.Data.Inventory.AddItem(id, count);
    }
    private void OnInventoryChange(string arg0, int arg1)
    {
        _audio.PlayOneShot(_collectItem);
    }

    public void SpawnGuard()
    {
        var coinCount = _gameSession.Data.Inventory.Count("coin");
        if (coinCount >= 5)
        {
            _gameSession.Data.Inventory.RemoveItem("coin", 5);
            var index = UnityEngine.Random.Range(0, _guardPrefabs.Length);
            SpawnUtil.Spawn(_guardPrefabs[index], _guardSpawner.gameObject.transform.position);
        }
    }

    public void Interact()
    {
        _intercationCheck.Check();
    }

    public void PowerUp(GameObject powered)
    {
        _bulletSpawer.SetPrefab(powered);
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
