using UnityEngine;
using UnityEngine.UI;

namespace UI.View.UI
{
    public class ShopView : CanvasView
    {
        [SerializeField] private Image _sellImage;
        [SerializeField] private Dropdown _sellSelector;
        [SerializeField] private InputField _sellInput;
        [Space]
        [SerializeField] private Image _buyImage;
        [SerializeField] private InputField _buyInput;
        [SerializeField] private Dropdown _buySelector;
        [Space]
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _exchangeButton;
        
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