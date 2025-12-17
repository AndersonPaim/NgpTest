using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _quantityText;

    private Item _itemData;
    
    public Item ItemData => _itemData;

    public int ItemSlot;

    public void Initialize(Item itemData, InventorySaveData savedData, int itemSlot)
    {
        _itemData = itemData;
        ItemSlot = itemSlot;
        Sprite icon = InventoryManager.Instance.InventoryUI.GetItemSprite(itemData.ItemType);

        if (icon != null)
        {
            _itemIcon.sprite = icon;
        }

        if (savedData != null)
        {
            _quantityText.text = savedData.Quantity.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.Instance.InventoryUI.DragItem(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject pointerObject = eventData.pointerCurrentRaycast.gameObject;
        
        if (pointerObject.layer != LayerMask.NameToLayer("InventoryUI"))
        {
            InventoryManager.Instance.DropItem(_itemData, this);
        }
        else
        {
            InventoryManager.Instance.InventoryUI.SwapItem(this);
        }
        
        InventorySlot inventorySlot = pointerObject.GetComponent<InventorySlot>();

        if (inventorySlot != null)
        {
            if (!inventorySlot.HasItem())
            {
                inventorySlot.AddItem(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.Instance.InventoryUI.HoverItem(_itemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.InventoryUI.StopHoverItem(_itemData);
    }
}
