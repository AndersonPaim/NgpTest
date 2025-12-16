
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.UI;

    public class InventoryUI : ScreenBase
    {
        [SerializeField] private List<InventorySlot> _inventorySlots = new();
        [SerializeField] private ItemSpritesList _itemSprites;
        [SerializeField] private InventoryItem _inventoryItemPrefab;
        [SerializeField] private ItemTooltip _itemTooltip;
        [SerializeField] private Transform _uiPanel;
        [SerializeField] private Transform _draggableItem;
        [SerializeField] private float _openAnimDuration;
        
        private bool _isDraggingItem;
        private Transform _lastItemParent;
        private List<InventoryItem> _inventoryItems = new();

        public override void Open()
        {   
            _uiPanel.DOScale(Vector3.zero, 0);
            base.Open();
            _uiPanel.DOScale(Vector3.one, _openAnimDuration);
        }
        
        public override async void Close()
        {   
            _uiPanel.DOScale(Vector3.zero, _openAnimDuration);
            await UniTask.Delay(TimeSpan.FromSeconds(_openAnimDuration));
            base.Close();
        }

        public void HoverItem(Item item)
        {
            _itemTooltip.gameObject.SetActive(true);
            _itemTooltip.SetItem(item);
        }

        public void StopHoverItem(Item item)
        {
            _itemTooltip.gameObject.SetActive(false);
        }

        public void DragItem(InventoryItem item)
        {
            _lastItemParent = item.transform.parent;
            _isDraggingItem = true;
            item.GetComponent<Image>().raycastTarget = false;
            item.transform.SetParent(_draggableItem);
            item.transform.localPosition = Vector3.zero;
        }

        public void SwapItem(InventoryItem item)
        {
            _isDraggingItem = true;
            item.transform.SetParent(_lastItemParent);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<Image>().raycastTarget = true;
        }
        
        public Sprite GetItemSprite(ItemType itemType)
        {
            return _itemSprites.Sprites.Find((x) => x.ItemType == itemType).Sprite;
        }

        private void OnEnable()
        {
            CreateInventoryUI();
        }

        private void Start()
        {
            InitializeInventoryUI();
        }
        
        private void Update()
        {
            MoveItem();
        }

        private void MoveItem()
        {
            Vector2 mousePos = Input.mousePosition;
            
            _itemTooltip.transform.position = mousePos;
            
            if (!_isDraggingItem)
            {
                return;
            }

            _draggableItem.position = mousePos;
        }

        private void InitializeInventoryUI()
        {
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                _inventorySlots[i].Initialize(i);
            }
        }

        private void CreateInventoryUI()
        {
            ClearInventoryUI();
            
            SaveData saveData = SaveSystem.localData;
            
            for (int i = 0; i < saveData.InventoryItems.Count; i++)
            {
                InventorySaveData inventorySaveData = saveData.InventoryItems[i];
                Debug.Log("Creating inventory UI: " + inventorySaveData.Slot);
                
                InventorySlot slot = _inventorySlots[inventorySaveData.Slot];
                InventoryItem item = Instantiate(_inventoryItemPrefab, slot.transform);
                item.Initialize(inventorySaveData.ItemData);
                _inventoryItems.Add(item);
                slot.AddItem(item);
            }
        }
        
        private void ClearInventoryUI()
        {
            foreach (InventoryItem slot in _inventoryItems)
            {
                if (slot != null)
                {
                    Destroy(slot.gameObject);
                }
            }

            _inventoryItems.Clear();
        }
    }
