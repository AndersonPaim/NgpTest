using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public Action OnInventoryUpdate;

    public List<InventorySaveData> InventoryItems = new();

    public SaveData()
    {

    }
}