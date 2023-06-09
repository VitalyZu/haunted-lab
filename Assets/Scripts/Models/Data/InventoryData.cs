using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventoryData
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();
    public UnityAction<string, int> OnChange;

    public void AddItem(string id, int value)
    {
        if (value <= 0) return;

        var itemDefs = DefFacade.I.Items.Get(id);
        if (itemDefs.IsVoid) return;

        var item = GetItemData(id);

        if (item == null)
        {
            item = new InventoryItemData(id);
            _inventory.Add(item);

        }
        item.Value += value;
        OnChange?.Invoke(id, Count(id));
    }

    public void RemoveItem(string id, int value)
    {
        var itemDefs = DefFacade.I.Items.Get(id);
        if (itemDefs.IsVoid) return;

        var item = GetItemData(id);

        if (item == null) return;

        item.Value -= value;
        if (item.Value <= 0) _inventory.Remove(item);
        OnChange?.Invoke(id, Count(id));
    }

    private InventoryItemData GetItemData(string id)
    {
        foreach (var item in _inventory)
        {
            if (item.Id == id) return item;
        }
        return null;
    }

    public int Count(string id)
    {
        int count = 0;
        foreach (var item in _inventory)
        {
            if (item.Id == id) count += item.Value;
        }
        return count;
    }
}

[Serializable]
public class InventoryItemData
{
    public string Id;
    public int Value;

    public InventoryItemData(string id)
    {
        Id = id;
    }
}
