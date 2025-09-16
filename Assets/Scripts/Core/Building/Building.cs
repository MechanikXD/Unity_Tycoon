using System.Collections.Generic;
using Core.Resource;
using Player.Interactable;
using UI;
using UI.HUD.View;
using UnityEngine;

namespace Core.Building
{
    public abstract class Building : MonoBehaviour, ISceneInteractable
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Color _notPlaceableColor = Color.red;
        private Color _originalColor;

        [Space]
        [SerializeField] private bool _isSingleton;
        [SerializeField] private float _halfHeight;
        [SerializeField] private ResourceBundle _buildCost;
        [SerializeField] private ResourceBundle[] _upgradeCosts;
        [SerializeField] private float _refundPercent;
        
        private int _currentUpgradeLevel;
        private ResourceBundle _resourcesSpent;
        private bool _isStationary;
        
        private static LayerMask _groundLayer;
        private HashSet<int> _objectsInRange;
        public bool CanBePlaced { get; private set; }
        public bool IsSingleton => _isSingleton;

        public float HalfHeight => _halfHeight;

        public virtual void Build()
        {
            UpdateGhostColor(default);
            if (!CanBePlaced) return;
            
            ResourceManager.ResourceUpdated -= UpdateGhostColor;
            ResourceManager.Instance.Spend(_buildCost);
            _resourcesSpent += _buildCost;
            
            RepositionEnd();
            
            BuildingManager.Instance.Add(this);
        }

        public virtual void RepositionStart()
        {
            _isStationary = false;
            UpdateGhostColor(default);
        }

        public virtual void RepositionEnd()
        {
            _renderer.material.color = _originalColor;
            _isStationary = true;
        }

        public virtual void Destroy()
        {
            var refund = new ResourceBundle
            {
                Gold = (int)(_resourcesSpent.Gold * _refundPercent),
                Wood = (int)(_resourcesSpent.Wood * _refundPercent),
                Stone = (int)(_resourcesSpent.Stone * _refundPercent),
                Ore = (int)(_resourcesSpent.Ore * _refundPercent),
                People = _resourcesSpent.People
            };
            ResourceManager.Instance.AddResources(refund);
        }

        public virtual void Upgrade()
        {
            if (_currentUpgradeLevel >= _upgradeCosts.Length) return;
            
            var upgradeCosts = _upgradeCosts[_currentUpgradeLevel];
            if (ResourceManager.Instance.HasEnoughResources(upgradeCosts))
            {
                _resourcesSpent += upgradeCosts;
                ResourceManager.Instance.Spend(upgradeCosts);
                _currentUpgradeLevel++;
            }
        }

        private void Awake()
        {
            _objectsInRange = new HashSet<int>();
            _originalColor = _renderer.material.color;
            _isStationary = false;
            UpdateGhostColor(default);
            
            _groundLayer = LayerMask.NameToLayer("Ground");
            ResourceManager.ResourceUpdated += UpdateGhostColor;
        }

        private void OnDisable()
        {
            ResourceManager.ResourceUpdated -= UpdateGhostColor;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isStationary || other.gameObject.layer == _groundLayer) return;
            
            _objectsInRange.Add(other.gameObject.GetInstanceID());
            _renderer.material.color = _notPlaceableColor;
            CanBePlaced = false;
        }

        private void OnCollisionExit(Collision other)
        {
            if (_isStationary) return;
            
            _objectsInRange.Remove(other.gameObject.GetInstanceID());
            if (_objectsInRange.Count == 0)
            {
                UpdateGhostColor(default);
            }
        }

        private void UpdateGhostColor(ResourceBundle _)
        {
            if (_isStationary) return;

            CanBePlaced = ResourceManager.Instance.HasEnoughResources(_buildCost) &&
                          _objectsInRange.Count == 0;
            CanBePlaced = IsSingleton ? !BuildingManager.Instance.HasSingleton(GetType()) : CanBePlaced;
            
            _renderer.material.color = CanBePlaced ? _originalColor : _notPlaceableColor;
        }

        public virtual void PrimaryAction()
        {
            BuildInteractionView.SetBuilding(this);
            UIManager.Instance.EnterHUDCanvas<BuildInteractionView>();
        }

        public virtual void SecondaryAction() {}
    }
}