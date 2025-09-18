using System;
using Core.Resource;
using TMPro;
using UI.View.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.HUD
{
    public class StatusBarView : CanvasView
    {
        private Action _eventUnSubscriber;
        [SerializeField] private Button _menuButton;
        
        [SerializeField] private TMP_Text _peopleCount;
        [SerializeField] private TMP_Text _populationCount;
        [SerializeField] private TMP_Text _goldCount;
        [SerializeField] private TMP_Text _woodCount;
        [SerializeField] private TMP_Text _stoneCount;
        [SerializeField] private TMP_Text _oreCount;

        private void OnEnable()
        {
            void EnterMenu() => UIManager.Instance.EnterUICanvas<MenuView>();
            _menuButton.onClick.AddListener(EnterMenu);
            _eventUnSubscriber = () =>
            {
                _menuButton.onClick.RemoveListener(EnterMenu);
            };
            ResourceManager.ResourceUpdated += UpdateResourceCount;
            UpdateResourceCount(ResourceManager.Instance.Current);
        }

        private void OnDisable()
        {
            ResourceManager.ResourceUpdated -= UpdateResourceCount;
            _eventUnSubscriber();
        }

        private void UpdateResourceCount(ResourceBundle bundle)
        {
            var people = ResourceManager.Instance.WorkingPopulation;
            var population = ResourceManager.Instance.MaxPopulation;
            Func<long, string> format = TextFormatHelper.NumberToCompactString;
            
            _peopleCount.SetText(format(people));
            _populationCount.SetText(format(population));
            _goldCount.SetText(format(bundle.Gold));
            _woodCount.SetText(format(bundle.Wood));
            _stoneCount.SetText(format(bundle.Stone));
            _oreCount.SetText(format(bundle.Ore));
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