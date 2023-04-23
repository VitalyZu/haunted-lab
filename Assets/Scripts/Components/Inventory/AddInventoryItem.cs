using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private int _count;

    public void Add(GameObject go)
    {
        var hero = go.GetComponent<Hero>();
        hero?.AddInInventory(_id, _count);
    }
}
