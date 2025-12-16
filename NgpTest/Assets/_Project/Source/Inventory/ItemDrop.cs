using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private ItemSpritesList  _itemSprites;
    [SerializeField] private Image _itemIcon;
    private Item _item;
    
    public void SetItem(Item item)
    {
        _item = item;
        _itemIcon.sprite = GetItemSprite(_item.ItemType);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if ((_collisionLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            if (InventoryManager.Instance.HasSlotAvailable())
            {
                InventoryManager.Instance.AddItemToInventory(_item);
                gameObject.SetActive(false);
            }
        }
    }
    
    private Sprite GetItemSprite(ItemType itemType)
    {
        return _itemSprites.Sprites.Find((x) => x.ItemType == itemType).Sprite;
    }
}
