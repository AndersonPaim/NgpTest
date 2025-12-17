using System;
using NaughtyAttributes;
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
    [ShowIf("SlotType", SlotType.Consumable)]
    public int EffectValue;
    [ShowIf("SlotType", SlotType.Consumable)]
    public EffectType EffectType;
    public int MaxStack;
    public int Quantity;
    public string Description;
    public string Name;
}