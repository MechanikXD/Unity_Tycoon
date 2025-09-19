using System.Collections.Generic;
using Core.Resource;
using UI.Data;
using UI.View.Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class CraftView : CanvasView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private ItemDisplay _display;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _craftButton;
        [Space]
        [SerializeField] private ItemDataSet _itemData;
        [SerializeField] private OnlyIconDisplay _itemPrefab;

        private Dictionary<ItemData, OnlyIconDisplay> _icons;

        private void Awake()
        {
            _display.Initialize(true);
            _icons = new Dictionary<ItemData, OnlyIconDisplay>();
            if (_itemData.DataSet is not { Length: > 0 }) return;
            
            foreach (var item in _itemData.DataSet)
            {
                if (item.IsOwned) continue;
                
                var instance = Instantiate(_itemPrefab, _content);
                instance.Set(item);
                instance.Initialize(_display, true);
                _icons.Add(item, instance);
            }
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(UIManager.Instance.ExitLastCanvas);
            _craftButton.onClick.AddListener(CraftItem);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(UIManager.Instance.ExitLastCanvas);
            _craftButton.onClick.RemoveListener(CraftItem);
        }

        private void CraftItem()
        {
            var current = _display.Current;
            if (!_craftButton.interactable || current == null) return;
            if (!ResourceManager.Instance.HasEnoughResources(current.Cost)) return;
            
            ResourceManager.Instance.Spend(current.Cost);
            current.IsOwned = true;
            _display.Current.Action.ItemOwned();
            
            var thisInstance = _icons[current];
            UIManager.Instance.GetUICanvas<InventoryView>().AddToInventory(thisInstance);
            _display.Clear();
        }

        public override void Show()
        {
            _display.Clear();
            _thisCanvas.enabled = true;
        }

        public override void Hide()
        {
            _thisCanvas.enabled = false;
        }
    }
}