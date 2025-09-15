using System;
using Core.Building;
using Player.Interactable;
using Player.Interactable.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.View
{
    public class BuildInteractionView : CanvasView
    {
        private static Building _currentBuilding;
        private Action _eventUnSubscriber;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private Vector3 _offset;
        
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _repositionButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _textField;

        // TODO: This should be done via UIManager

        private void OnEnable()
        {
            void UpgradeProxy()
            {
                _currentBuilding.OnUpgrade();
            }
            
            void RepositionBuild()
            {
                ObjectRepositionState.SetObjectToMove(_currentBuilding);
                InteractionTrigger.EnterState<ObjectRepositionState>();
            }

            void RemoveProxy()
            {
                _currentBuilding.OnRemove();
            }
            
            _upgradeButton.onClick.AddListener(UpgradeProxy);
            _repositionButton.onClick.AddListener(RepositionBuild);
            _destroyButton.onClick.AddListener(RemoveProxy);

            _eventUnSubscriber = () =>
            {
                _upgradeButton.onClick.RemoveListener(UpgradeProxy);
                _repositionButton.onClick.RemoveListener(RepositionBuild);
                _destroyButton.onClick.RemoveListener(RemoveProxy);
            };
        }
        
        private void OnDisable()
        {
            _eventUnSubscriber();
        }

        public static void SetBuilding(Building building)
        {
            _currentBuilding = building;
        }

        public override void Show()
        {
            _contentTransform.position = Input.mousePosition + _offset;
            _thisCanvas.enabled = true;
        }

        public override void Hide()
        {
            _currentBuilding = null;
            _thisCanvas.enabled = false;
        }
    }
}