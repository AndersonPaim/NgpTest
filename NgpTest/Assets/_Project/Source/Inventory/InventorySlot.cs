using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private InventoryItem _item;
    private int _slotIndex;

    public void Initialize(int slotIndex)
    {
        _slotIndex = slotIndex;
    }

    public void AddItem(InventoryItem item)
    {
        SaveData saveData = SaveSystem.localData;
        item.transform.SetParent(transform);;
        item.transform.localPosition = Vector3.zero;
        _item = item;

        Debug.Log("CHANGE ITEM SLOT FROM: " + item.ItemData.Slot);
        InventorySaveData inventorySaveData = saveData.InventoryItems.Find((x) => x.ItemData == item.ItemData);
        
        foreach(InventorySaveData slot in saveData.InventoryItems)
        {
            Debug.Log("slot filled: " + slot.Slot);
        }

        if (inventorySaveData != null)
        {
            inventorySaveData.Slot = _slotIndex;
            item.ItemData.Slot = _slotIndex;
            
            Debug.Log("CHANGE ITEM SLOT TO: " + inventorySaveData.Slot);
        }
        
        SaveSystem.Save();
    }

    public bool HasItem()
    {
        return _item != null;
    }
}
