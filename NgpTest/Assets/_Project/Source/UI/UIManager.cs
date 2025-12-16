using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Source.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ScreenBase _inventoryUI;
        
        private PlayerInputActions _input;

        private ScreenBase _currentScreen;

        private void Start()
        {
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
            _input.Player.Inventory.performed += ToggleInventoryUI;
            _input.Player.Escape.performed += CloseCurrentScreen;
        }

        private void DestroyEvents()
        {
            _input.Player.Inventory.performed -= ToggleInventoryUI;
        }
        
        private void ToggleInventoryUI(InputAction.CallbackContext ctx)
        {
            if (_inventoryUI.gameObject.activeSelf)
            {
                _currentScreen = null;
                _inventoryUI.Close();
            }
            else
            {
                _currentScreen = _inventoryUI;
                _inventoryUI.Open();
            }
        }

        private void CloseCurrentScreen(InputAction.CallbackContext ctx)
        {
            _currentScreen.Close();
        }
    }
}