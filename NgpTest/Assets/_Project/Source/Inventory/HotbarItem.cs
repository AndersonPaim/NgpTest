using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _quantityText;
    
    public void Initialize(Item itemData, InventorySaveData savedData)
    {
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
}
