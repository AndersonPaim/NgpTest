using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryItem _inventoryItemPrefab;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Transform _draggableItem;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private ItemDrop _itemDropPrefab;
    [SerializeField] private int _inventorySlots;
    [SerializeField] private float _dropRadius;
    
    public InventoryUI InventoryUI => _inventoryUI;
    public static InventoryManager Instance;
    

    public bool HasSlotAvailable()
    {
        SaveData saveData = SaveSystem.localData;
        return saveData.InventoryItems.Count < _inventorySlots;
    }

    [Button]
    public void CheckInventory()
    {
        SaveData saveData = SaveSystem.localData;
        
        foreach (InventorySaveData slot in saveData.InventoryItems)
        {
            Debug.Log("INVENTORY SLOT:" + slot.ItemData.ItemType + " : " + slot.ItemData.Quantity + " :  " + slot.ItemData.Slot);
        }
    }

    public void DropItem(Item item, InventoryItem inventoryItem)
    {
        Debug.Log("DROP ITEM: " + item.Name);
        ItemDrop itemDrop = ObjectPooler.Instance.SpawnFromPool(_itemDropPrefab.gameObject).GetComponent<ItemDrop>();
        itemDrop.SetItem(item);
        Destroy(inventoryItem.gameObject);
        
        Vector3 randomPosition = _player.transform.position + Random.insideUnitSphere * _dropRadius;
        randomPosition.y = 1;
        itemDrop.transform.position = randomPosition;
        
        SaveData saveData = SaveSystem.localData;
        InventorySaveData existingItem = saveData.InventoryItems.Find(x => x.ItemData.ItemType == item.ItemType);
        saveData.InventoryItems.Remove(existingItem);
        saveData.InventoryItems.Sort((data1, data2) => data1.Slot.CompareTo(data2.Slot));
        SaveSystem.Save();
    }

    public void AddItemToInventory(Item item)
    {
        Debug.Log("ADD ITEM: " + item.Name);
        SaveData saveData = SaveSystem.localData;

        if (saveData.InventoryItems.Count < _inventorySlots)
        {
            InventorySaveData existingItem = saveData.InventoryItems.Find(x => x.ItemData.ItemType == item.ItemType);

            if (existingItem == null)
            {
                int slot = GetNextInventorySlot();
                item.Slot = slot;
                
                saveData.InventoryItems.Add(new InventorySaveData
                {
                    ItemData = item,
                    Slot = slot
                });
            }
            else
            {
                int spaceAvailable = item.MaxStack - existingItem.ItemData.Quantity;

                if (existingItem.ItemData.Quantity <= spaceAvailable)
                {
                    existingItem.ItemData.Quantity += item.Quantity;
                }
                else
                {
                    existingItem.ItemData.Quantity = item.MaxStack;

                    int remainingQuantity = item.Quantity - spaceAvailable;
                    
                    while (remainingQuantity > 0)
                    {
                        int quantityToAdd = Mathf.Min(remainingQuantity, item.MaxStack);
                        
                        int slot = GetNextInventorySlot();
                        item.Slot = slot;
                        
                        saveData.InventoryItems.Add(new InventorySaveData
                        {
                            ItemData = item,
                            Slot = slot
                        });

                        remainingQuantity -= quantityToAdd;
                    }
                }
            }
            
            saveData.InventoryItems.Sort((data1, data2) => data1.Slot.CompareTo(data2.Slot));
            SaveSystem.Save();
        }
    }

    private int GetNextInventorySlot()
    {
        SaveData saveData = SaveSystem.localData;

        for (int i = 0; i < _inventorySlots; i++)
        {
            if (saveData.InventoryItems.Find((x) => x.Slot == i) == null)
            {
                return i;
            }
        }

        return 0;
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            Instance = this;
        }

        SaveSystem.Load();
    }
}
