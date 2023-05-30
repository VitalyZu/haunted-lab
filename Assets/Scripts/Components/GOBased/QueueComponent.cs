using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueComponent : MonoBehaviour
{
    [SerializeField] private float _length;
    public static Queue<GameObject> queue = new Queue<GameObject>();

    private void Start()
    {
        queue.Enqueue(gameObject);
        if (queue.Count > _length)
        {
            Destroy(queue.Dequeue());
        }
    }
}
