using UnityEngine;
using UnityEngine.UI;

namespace UI.View.HUD
{
    public class MainButtonsView : CanvasView
    {
        [SerializeField] private Button _buildButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _craftButton;
        [SerializeField] private Button _inventoryButton;
        
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