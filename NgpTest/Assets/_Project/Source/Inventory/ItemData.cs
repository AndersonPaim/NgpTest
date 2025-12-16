using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public Item Item;
}

[Serializable]
public class DropData
{
    public int DropChance;
    public GameObject DropPrefab;
    public ItemData ItemData;
}

[Serializable]
public class Item
{
    public ItemType ItemType;
    public SlotType SlotType;
    public int MaxStack;
    public int Quantity;
    [HideInInspector] public int Slot;
    public string Description;
    public string Name;
}