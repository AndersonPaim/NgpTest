using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private InventoryItem _item;
    private int _slotIndex;

    public InventoryItem Item;

    public void RemoveItem()
    {
        _item = null;
    }

    public void Initialize(int slotIndex)
    {
        _slotIndex = slotIndex;
    }

    public void AttachItem(InventoryItem item)
    {
        item.transform.SetParent(transform);;
        item.transform.localPosition = Vector3.zero;
        _item = item;
    }

    public void AddItem(InventoryItem item)
    {
        SaveData saveData = SaveSystem.localData;
        AttachItem(item);

        InventorySaveData inventorySaveData = saveData.InventoryItems.Find((x) => x.Slot == item.ItemSlot);
        item.ItemSlot = _slotIndex;
        
        if (inventorySaveData != null)
        {
            Debug.Log("CHANGE ITEM SLOT FROM: " + item.ItemSlot + " TO: " + _slotIndex);
            inventorySaveData.Slot = _slotIndex;
        }
        
        SaveSystem.Save();
    }

    public bool HasItem()
    {
        return _item != null;
    }
}
