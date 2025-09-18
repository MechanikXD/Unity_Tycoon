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

        private ItemData _currentDisplay;

        private void Awake()
        {
            if (_itemData.DataSet is not { Length: > 0 }) return;
            
            foreach (var item in _itemData.DataSet)
            {
                var instance = Instantiate(_itemPrefab, _content);
                instance.Set(item);
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
            if (!_craftButton.interactable || _currentDisplay == null) return;
            if (!ResourceManager.Instance.HasEnoughResources(_currentDisplay.Cost)) return;
            
            ResourceManager.Instance.Spend(_currentDisplay.Cost);
            _currentDisplay.IsOwned = true;
            // TODO: Remove item from this content
            // TODO: Item obtaining logic
            // _currentDisplay.OnItemOwned();
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