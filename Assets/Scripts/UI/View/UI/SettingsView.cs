using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class SettingsView : CanvasView
    {
        [SerializeField] private Button _exitButton;
        
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