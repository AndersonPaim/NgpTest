
    using System.Collections.Generic;
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class HotbarManager :  MonoBehaviour
    {
        [SerializeField] private List<GameObject> _inventorySlots = new();
        [SerializeField] private ItemSpritesList _itemSprites;
        [SerializeField] private HotbarItem _hotbarItemPrefab;
        [SerializeField] private ConsumablesController _consumablesController;
        
        private List<HotbarItem> _hotbarItems = new();
        private PlayerInputActions _input;

        private void Start()
        {
            CreateHotbarUI();
            SetupEvents();
        }

        private void OnDestroy()
        {
            DestroyEvents();
        }

        private void SetupEvents()
        {
            _input = new PlayerInputActions();
            _input.Enable();
            SaveSystem.OnSaveData += UpdateHotbarUI;
            _input.Player.Num1.performed += ctx  => UseHotbar(ctx ,1);
            _input.Player.Num2.performed += ctx  => UseHotbar(ctx ,2);
            _input.Player.Num3.performed += ctx  => UseHotbar(ctx ,3);
            _input.Player.Num4.performed += ctx  => UseHotbar(ctx ,4);
            _input.Player.Num5.performed += ctx  => UseHotbar(ctx ,5);
            _input.Player.Num6.performed += ctx  => UseHotbar(ctx ,6);
            _input.Player.Num7.performed += ctx  => UseHotbar(ctx ,7);
        }
        
        private void DestroyEvents()
        {
            SaveSystem.OnSaveData -= UpdateHotbarUI;
        }

        private void UseHotbar(InputAction.CallbackContext ctx, int index)
        {
            GameObject slot = _inventorySlots[index - 1];
            
            slot.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f).SetUpdate(true).OnComplete
            (
                () => slot.transform.DOScale(Vector3.one, 0.1f)
            );
            
            SaveData saveData = SaveSystem.localData;
            InventorySaveData savedData = saveData.InventoryItems.Find((x) => x.Slot == index - 1);

            if (savedData != null)
            {
                if (savedData.ItemData.SlotType == SlotType.Consumable)
                {
                    _consumablesController.UseConsumable(savedData.ItemData.ItemType);
                    InventoryManager.Instance.ConsumeItem(index - 1);
                }
            }
            
            SaveSystem.Save();
        }

        private void UpdateHotbarUI()
        {
            CreateHotbarUI();
        }
        
        private void CreateHotbarUI()
        {
            ClearHotbarUI();
            SaveData saveData = SaveSystem.localData;
            
            for (int i = 0; i < saveData.InventoryItems.Count; i++)
            {
                if (saveData.InventoryItems[i].Slot <= _inventorySlots.Count)
                {
                    InventorySaveData inventorySaveData = saveData.InventoryItems[i];
                    GameObject slot = _inventorySlots[inventorySaveData.Slot];
                    HotbarItem item = Instantiate(_hotbarItemPrefab, slot.transform);
                    item.Initialize(inventorySaveData.ItemData, inventorySaveData);
                    _hotbarItems.Add(item);
                    item.transform.SetParent(slot.transform);
                    item.transform.localPosition = Vector3.zero;
                }
            }
        }
        
        private void ClearHotbarUI()
        {
            foreach (HotbarItem item in _hotbarItems)
            {
                if (item != null)
                {
                    item.gameObject.SetActive(false);
                }
            }

            _hotbarItems.Clear();
        }
    }
