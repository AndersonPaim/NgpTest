using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _inventorySlots = new();
    [SerializeField] private List<ItemSprites> _itemSprites = new();
    [SerializeField] private InventoryItem _inventoryItemPrefab;
    [SerializeField] private Transform _draggableItem;

    private bool _isDraggingItem;
    private Transform _lastItemParent;

    public static InventoryManager Instance;

    public void DragItem(InventoryItem item)
    {
        _lastItemParent = item.transform.parent;
        _isDraggingItem = true;
        item.GetComponent<Image>().raycastTarget = false;
        item.transform.SetParent(_draggableItem);
        item.transform.localPosition = Vector3.zero;
    }

    public void DropItem(InventoryItem item)
    {
        _isDraggingItem = true;
        item.transform.SetParent(_lastItemParent);
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<Image>().raycastTarget = true;
    }

    public bool HasSlotAvailable()
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (!slot.HasItem())
            {
                return true;
            }
        }

        return false;
    }

    public void AddItemToInventory(ItemData itemData)
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (!slot.HasItem())
            {
                InventoryItem item = Instantiate(_inventoryItemPrefab, slot.transform);
                item.Initialize(itemData);
                slot.AddItem(item);
                break;
            }
        }
    }

    public Sprite GetItemSprite(ItemType itemType)
    {
        return _itemSprites.Find((x) => x.ItemType == itemType).Sprite;
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
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        MoveItem();
    }

    private void Initialize()
    {

    }

    private void MoveItem()
    {
        if (!_isDraggingItem)
        {
            return;
        }

        Vector2 mousePos = Input.mousePosition;
        _draggableItem.position = mousePos;
    }
}
