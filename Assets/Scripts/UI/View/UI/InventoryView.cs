using UI.Data;
using UI.View.Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class InventoryView : CanvasView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private ItemDisplay _display;
        [SerializeField] private Button _exitButton;
        
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
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(UIManager.Instance.ExitLastCanvas);
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