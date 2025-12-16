using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private ItemData _item;

    private void OnTriggerEnter(Collider other)
    {
        if (InventoryManager.Instance.HasSlotAvailable())
        {
            InventoryManager.Instance.AddItemToInventory(_item);
            gameObject.SetActive(false);
        }
    }
}
