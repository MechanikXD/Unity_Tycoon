using UI.BuildingTab;
using UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.HUD
{
    public class BuildCanvasView : CanvasView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Button _exitButton;
        
        [SerializeField] private BuildingDataSet _buildingData;
        [SerializeField] private BuildIcon _contentPrefab;
        
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