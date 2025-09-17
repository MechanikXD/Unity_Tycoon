using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class SettingsView : CanvasView
    {
        [SerializeField] private Button _exitButton;
        
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