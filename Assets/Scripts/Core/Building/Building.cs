using System.Collections.Generic;
using Core.Audio;
using Core.Resource;
using Player.Interactable;
using UI;
using UI.View.HUD;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Building
{
    public abstract class Building : MonoBehaviour, ISceneInteractable
    {
        [FormerlySerializedAs("_prefabIdInDatSet"),SerializeField] private int _prefabIdInDataSet;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Color _notPlaceableColor = Color.red;
        private Color _originalColor;
        [SerializeField] private string _description = $"{ResourceManager.PeopleTextIcon} <- cool icon";
        [SerializeField] private AudioClip _placeSound;
        [SerializeField] private AudioClip _destroySound;
        
        [Space]
        [SerializeField] private bool _isSingleton;
        [SerializeField] private float _halfHeight;
        [SerializeField] private ResourceBundle _buildCost;
        [SerializeField] private ResourceBundle[] _upgradeCosts;
        [SerializeField] private float _refundPercent;
        
        private ResourceBundle _resourcesSpent;
        private bool _isStationary;
        
        private static LayerMask _groundLayer;
        private HashSet<int> _objectsInRange;

        public int PrefabIdInDataSet => _prefabIdInDataSet;
        public ResourceBundle Cost => _buildCost;
        public int CurrentUpgradeLevel { get; private set; }
        public bool CanBePlaced { get; private set; }
        public bool IsSingleton => _isSingleton;
        public float HalfHeight => _halfHeight;
        public string Description => _description;
        public bool CanBeUpgraded => CurrentUpgradeLevel < _upgradeCosts.Length;
        public virtual bool CanBeDestroyed => true; 

        public virtual void Build()
        {
            ResourceManager.ResourceUpdated -= UpdateGhostColor;
            ResourceManager.Instance.Spend(_buildCost);
            _resourcesSpent += _buildCost;
            
            RepositionEnd();
            
            AudioManager.Instance.PlaySound(_placeSound, transform.position);
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

        protected virtual void BeforeDestroy() { }
        
        public void Destroy()
        {
            if (!CanBeDestroyed) return;
            
            var refund = new ResourceBundle
            {
                Gold = (int)(_resourcesSpent.Gold * _refundPercent),
                Wood = (int)(_resourcesSpent.Wood * _refundPercent),
                Stone = (int)(_resourcesSpent.Stone * _refundPercent),
                Ore = (int)(_resourcesSpent.Ore * _refundPercent),
                People = _resourcesSpent.People
            };
            ResourceManager.Instance.AddResources(refund);
            BuildingManager.Instance.Remove(this);
            UIManager.Instance.ExitHudCanvas<BuildInteractionView>();
            AudioManager.Instance.PlaySound(_destroySound, transform.position);
            BeforeDestroy();
            Destroy(gameObject);
        }

        public virtual void Upgrade()
        {
            if (!CanBeUpgraded) return;
            
            var upgradeCosts = _upgradeCosts[CurrentUpgradeLevel];
            if (ResourceManager.Instance.HasEnoughResources(upgradeCosts))
            {
                _resourcesSpent += upgradeCosts;
                ResourceManager.Instance.Spend(upgradeCosts);
                CurrentUpgradeLevel++;
            }
        }

        protected virtual void Awake()
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
            
            if (IsSingleton && BuildingManager.Instance.HasSingleton(GetType()))
            {
                CanBePlaced = false;
            }
            
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