using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public ItemType ItemType;
    public SlotType SlotType;
    public int MaxStack;
    public int Quantity;
}