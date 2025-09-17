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