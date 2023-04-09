using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUtil : MonoBehaviour
{
    private const string ContainerName = "###SPAWNED###";

    public static GameObject Spawn(GameObject prefab, Vector3 position, string containerName = ContainerName)
    {
        var container = GameObject.Find(containerName);
        if (container == null)
            container = new GameObject(containerName);

        return Object.Instantiate(prefab, position, Quaternion.identity, container.transform);
    }
}
