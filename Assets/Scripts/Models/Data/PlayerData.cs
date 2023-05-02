using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private InventoryData _inventory;
    public InventoryData Inventory => _inventory;
    public IntProperty HP = new IntProperty();
}
