using System;
using UI.View.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.HUD
{
    public class MainButtonsView : CanvasView
    {
        private Action _eventUnSubscriber;
        
        [SerializeField] private Button _buildButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _craftButton;
        [SerializeField] private Button _inventoryButton;

        public Button ShopButton => _shopButton;
        public Button CraftButton => _craftButton;
        
        private void Awake()
        {
            _craftButton.interactable = false;
            _shopButton.interactable = false;
        }

        private void OnEnable()
        {
            void EnterBuildMode() => UIManager.Instance.EnterHUDCanvas<BuildModeView>();
            void EnterInventory() => UIManager.Instance.EnterUICanvas<InventoryView>();
            void EnterCraft() => UIManager.Instance.EnterUICanvas<CraftView>();
            void EnterShop() => UIManager.Instance.EnterUICanvas<ShopView>();
            
            _buildButton.onClick.AddListener(EnterBuildMode);
            _inventoryButton.onClick.AddListener(EnterInventory);
            _craftButton.onClick.AddListener(EnterCraft);
            _shopButton.onClick.AddListener(EnterShop);

            _eventUnSubscriber = () =>
            {
                _buildButton.onClick.RemoveListener(EnterBuildMode);
                _inventoryButton.onClick.RemoveListener(EnterInventory);
                _craftButton.onClick.RemoveListener(EnterCraft);
                _shopButton.onClick.RemoveListener(EnterShop);
            };
        }

        private void OnDisable()
        {
            _eventUnSubscriber();
        }

        public override void Show()
        {
            _thisCanvas.enabled = true;
        }

        public override void Hide()
        {
            _thisCanvas.enabled = false;
        }
    }
}