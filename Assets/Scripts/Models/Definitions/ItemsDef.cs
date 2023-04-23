using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/ItemsDef", fileName = "ItemsDef")]
public class ItemsDef : ScriptableObject
{
    [SerializeField] ItemDef[] _items;

    public ItemDef Get(string id)
    {
        foreach (var item in _items)
        {
            if (item.Id == id) return item;
        }
        return default;
    }
}

[Serializable]
public struct ItemDef 
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _icon;

    public string Id => _id;
    public bool IsVoid => string.IsNullOrEmpty(_id);
    public Sprite Icon => _icon;
}
