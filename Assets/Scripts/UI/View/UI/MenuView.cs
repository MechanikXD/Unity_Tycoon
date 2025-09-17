using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class MenuView : CanvasView
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _menuButton;
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