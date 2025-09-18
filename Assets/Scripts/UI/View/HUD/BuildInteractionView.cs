using System;
using Core.Building;
using Player.Interactable;
using Player.Interactable.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.HUD
{
    public class BuildInteractionView : CanvasView
    {
        private static Building _currentBuilding;
        private Action _eventUnSubscriber;
        [SerializeField] private RectTransform _contentTransform;

        private static Button _specialInteraction; 
        
        [SerializeField] private Button _specialInteractionButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _repositionButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _textField;

        private void Awake()
        {
            _specialInteraction = _specialInteractionButton;
            _specialInteractionButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            void UpgradeProxy()
            {
                _currentBuilding.Upgrade();
                if (!_currentBuilding.CanBeUpgraded) _upgradeButton.interactable = false;
            }
            
            void RepositionBuild()
            {
                ObjectRepositionState.SetObjectToMove(_currentBuilding);
                InteractionTrigger.EnterState<ObjectRepositionState>();
            }

            void RemoveProxy()
            {
                _currentBuilding.Destroy();
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

        public static void EnableSpecialInteractionButton(Action listener)
        {
            _specialInteraction.gameObject.SetActive(true);
            _specialInteraction.onClick.AddListener(() => listener());
        }

        public override void Show()
        {
            _contentTransform.position = Input.mousePosition;
            
            _textField.SetText(_currentBuilding.Description);
            _destroyButton.interactable = _currentBuilding.CanBeDestroyed;
            _upgradeButton.interactable = _currentBuilding.CanBeUpgraded;
            
            _thisCanvas.enabled = true;
        }

        public override void Hide()
        {
            _currentBuilding = null;
            _thisCanvas.enabled = false;

            if (_specialInteractionButton.gameObject.activeInHierarchy)
            {
                _specialInteractionButton.onClick.RemoveAllListeners();
                _specialInteractionButton.gameObject.SetActive(false);
            }
        }
    }
}