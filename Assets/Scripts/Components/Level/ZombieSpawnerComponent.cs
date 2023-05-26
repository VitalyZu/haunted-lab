using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
            
            var instance = SpawnUtil.Spawn(prefab, transform.position);
            RandomizeInstance(instance);

            yield return new WaitForSeconds(delay);
        }
    }

    private void RandomizeInstance(GameObject instance)
    {
        var renderer = instance.GetComponent<SpriteRenderer>();
        SetLayerOrder(renderer);
        ChangeColor(renderer);
        ChangeSize(instance);
    }

    private void SetLayerOrder(SpriteRenderer renderer)
    {
        var currentOrder = renderer.sortingOrder;
        if (currentOrder <= _spawnManager.LayerOrder)
        {
            _spawnManager.LayerOrder++;
            var order = _spawnManager.LayerOrder;
            renderer.sortingOrder = order;
        }
    }

    private void ChangeColor(SpriteRenderer renderer)
    {
        renderer.color = new Color(UnityEngine.Random.Range(0.8f, 1f), UnityEngine.Random.Range(0.8f, 1f), UnityEngine.Random.Range(0.8f, 1f));
    }

    private void ChangeSize(GameObject instance)
    {
        instance.transform.localScale = new Vector3(UnityEngine.Random.Range(0.8f, 1.2f), UnityEngine.Random.Range(0.8f, 1.2f), 1f);
    }
}
