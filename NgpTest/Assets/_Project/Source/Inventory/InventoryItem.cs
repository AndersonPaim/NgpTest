using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _quantityText;

    public void Initialize(ItemData itemData)
    {
        Sprite icon = InventoryManager.Instance.GetItemSprite(itemData.ItemType);

        if (icon != null)
        {
            _itemIcon.sprite = icon;
            _quantityText.text = itemData.Quantity.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.Instance.DragItem(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.Instance.DropItem(this);

        InventorySlot inventorySlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();

        if (inventorySlot != null)
        {
            inventorySlot.AddItem(this);
        }

    }
}
