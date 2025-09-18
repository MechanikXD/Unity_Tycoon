using UI.Data;
using UI.View.Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.HUD
{
    public class BuildModeView : CanvasView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Button _exitButton;
        
        [SerializeField] private BuildingDataSet _buildingData;
        [SerializeField] private BuildIcon _contentPrefab;
        
        [SerializeField] private Color _allowBuildColor = new Color(.4f, .8f, 1f, .4f);
        [SerializeField] private Color _prohibitBuildColor = new Color(1f, .4f, .4f, .4f);

        private void Awake()
        {
            if (_buildingData.DataSet is not { Length: > 0 }) return;
            
            foreach (var build in _buildingData.DataSet)
            {
                var item = Instantiate(_contentPrefab, _content);
                item.Set(build.Icon, build.Cost, build.Prefab);
            }
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(Hide);
        }

        public override void Show()
        {
            UIManager.Instance.ExitHudCanvas<MainButtonsView>();
            _thisCanvas.enabled = true;
        }

        public override void Hide()
        {
            _thisCanvas.enabled = false;
            UIManager.Instance.EnterHUDCanvas<MainButtonsView>();
        }
    }
}