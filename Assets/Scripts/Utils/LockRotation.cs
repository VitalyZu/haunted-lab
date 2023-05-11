using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    [SerializeField] private Vector2 _direction;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(_direction);
    }
}
