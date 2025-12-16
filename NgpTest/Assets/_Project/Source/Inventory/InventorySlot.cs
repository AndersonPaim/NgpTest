using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem Item;

    public void Initialize()
    {

    }

    public void AddItem(InventoryItem item)
    {
        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
        Item = item;
    }

    public bool HasItem()
    {
        return Item != null;
    }
}
