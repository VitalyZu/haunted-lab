using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private readonly Dictionary<int, Queue<PoolItem>> _items = new Dictionary<int, Queue<PoolItem>>();
    private static Pool _instance;
    public static Pool Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("###MAIN_POOL###");
                _instance = go.AddComponent<Pool>();
            }
            return _instance;
        }
    }
    public GameObject Get(GameObject go, Transform target)
    {
        var id = go.GetInstanceID();
        var queue = RequireQueue(id);
        Debug.Log(id + "  asd");
        if (queue.Count > 0)
        {
            var pooledItem = queue.Dequeue();
            pooledItem.transform.position = target.position;
            pooledItem.gameObject.SetActive(true);
            pooledItem.Restart(target);
            return pooledItem.gameObject;
        }
        var instance = SpawnUtil.Spawn(go, target.position, gameObject.name);
        var poolItem = instance.GetComponent<PoolItem>();
        poolItem.Retain(id, this);

        return instance;
    }

    private Queue<PoolItem> RequireQueue(int id)
    {
        if (!_items.TryGetValue(id, out var queue))
        {
            queue = new Queue<PoolItem>();
            _items.Add(id, queue);
        }
        return queue;
    }

    public void Release(int id, PoolItem poolItem)
    {
        var queue = RequireQueue(id);
        queue.Enqueue(poolItem);
        poolItem.gameObject.SetActive(false);
    }
}
