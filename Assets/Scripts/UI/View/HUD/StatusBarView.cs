using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.HUD
{
    public class StatusBarView : CanvasView
    {
        [SerializeField] private Button _menuButton;
        
        [SerializeField] private TMP_Text _peopleCount;
        [SerializeField] private TMP_Text _populationCount;
        [SerializeField] private TMP_Text _goldCount;
        [SerializeField] private TMP_Text _woodCount;
        [SerializeField] private TMP_Text _stoneCount;
        [SerializeField] private TMP_Text _oreCount;
        
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