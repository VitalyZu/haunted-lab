using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolItem : MonoBehaviour
{
    private int _id;
    private Pool _pool;
    [SerializeField] private OnReloadPoolItemEvent _onReload;

    public void Retain(int id, Pool pool)
    {
        _id = id;
        _pool = pool;
    }

    public void Release()
    {
        _pool?.Release(_id, this);
    }

    public void Restart(Transform target)
    {
        _onReload?.Invoke(target);
    }
}

[Serializable]
public class OnReloadPoolItemEvent : UnityEvent<Transform>
{ }
