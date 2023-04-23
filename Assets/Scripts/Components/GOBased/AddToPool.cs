using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolItem))]
public class AddToPool : MonoBehaviour
{
    private void Start()
    {
        var pool = Pool.Instance;
        var poolItem = GetComponent<PoolItem>();
        var position = transform.position;
        poolItem.Retain(gameObject.GetInstanceID(), pool);

        gameObject.transform.parent = pool.GetComponent<Transform>();
        gameObject.transform.position = position;
    }
}
