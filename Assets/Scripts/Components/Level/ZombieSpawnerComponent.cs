using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerComponent : MonoBehaviour
{
    private ZombieSpawnerManager _spawnManager;
    private void Start()
    {
        _spawnManager = FindObjectOfType<ZombieSpawnerManager>();
    }
    public void Spawn(int number, GameObject[] types, float delay)
    {
        StartCoroutine(SpawnEnemies(number, types, delay));
    }

    private IEnumerator SpawnEnemies(int number, GameObject[] types, float delay)
    {
        for (int i = 0; i < number; i++)
        {
            var prefabIndex = (int)Mathf.Repeat(i, types.Length);
            var prefab = types[prefabIndex];
            //
            var renderer = prefab.GetComponent<SpriteRenderer>();
            var currentOrder = renderer.sortingOrder;
            if (currentOrder <= _spawnManager.LayerOrder)
            {
                _spawnManager.LayerOrder++;
                var order = _spawnManager.LayerOrder;
                renderer.sortingOrder = order;
            }
            //
            SpawnUtil.Spawn(prefab, transform.position);
            yield return new WaitForSeconds(delay);
        }
    }
}
