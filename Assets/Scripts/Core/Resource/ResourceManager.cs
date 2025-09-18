using System;
using System.Collections;
using Core.Behaviour.Singleton;
using UnityEngine;

namespace Core.Resource
{
    public class ResourceManager : SingletonBase<ResourceManager>
    {
        [SerializeField] private float _resourceUpdateTime;
        [SerializeField] private ResourceBundle _passiveIncome;
        private ResourceBundle _playerResources;

        [SerializeField] private int _maxPopulation;
        private int _workingPeople;
        
        private Coroutine _resourceUpdateCoroutine;
        private bool _coroutineCancellationToken;

        public int MaxPopulation => _maxPopulation;
        public int WorkingPopulation => _workingPeople;

        public static event Action<ResourceBundle> ResourceUpdated;

        public readonly static string WoodTextIcon = "<sprite=\"wood\" index=0>";
        public readonly static string StoneTextIcon = "<sprite=\"stone\" index=0>";
        public readonly static string GoldTextIcon = "<sprite=\"gold\" index=0>";
        public readonly static string OreTextIcon = "<sprite=\"ore\" index=0>";
        public readonly static string PeopleTextIcon = "<sprite=\"people\" index=0>";
        
        public ResourceBundle Current => _playerResources;

        protected override void Awake()
        {
            base.Awake();
            _resourceUpdateCoroutine = StartCoroutine(UpdateResourcesOnTimer());
            _playerResources.People = _maxPopulation;
        }
        
        public bool HasEnoughResources(ResourceBundle resources)
        {
            if (_maxPopulation - _workingPeople < resources.People) return false;

            return _playerResources.Gold >= resources.Gold &&
                   _playerResources.Wood >= resources.Wood &&
                   _playerResources.Stone >= resources.Stone &&
                   _playerResources.Ore >= resources.Ore &&
                   _maxPopulation - _workingPeople >= resources.People;
        }
        
        public bool HasEnoughResource(ResourceType ofType, int amount)
        {
            return _playerResources.Get(ofType) >= amount;
        }

        public void Spend(ResourceBundle resources)
        {
            if (!HasEnoughResources(resources)) return;

            _workingPeople += resources.People;
            _playerResources -= resources;
            
            ResourceUpdated?.Invoke(_playerResources);
        }

        public void AddResources(ResourceBundle resources)
        {
            _workingPeople -= resources.People;
            _playerResources += resources;

            ResourceUpdated?.Invoke(_playerResources);
        }
        
        public void AddMaxPopulation(int count)
        {
            _maxPopulation += count;
            _playerResources.People += count;

            ResourceUpdated?.Invoke(_playerResources);
        }

        public void AddIncome(ResourceType ofType, int increment)
        {
            switch (ofType)
            {
                case ResourceType.Gold:
                    _passiveIncome.Gold += increment;
                    break;
                case ResourceType.Wood:
                    _passiveIncome.Wood += increment;
                    break;
                case ResourceType.Stone:
                    _passiveIncome.Stone += increment;
                    break;
                case ResourceType.Ore:
                    _passiveIncome.Ore += increment;
                    break;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopCoroutine(_resourceUpdateCoroutine);
        }

        private IEnumerator UpdateResourcesOnTimer()
        {
            _coroutineCancellationToken = true;
            while (_coroutineCancellationToken)
            {
                yield return new WaitForSeconds(_resourceUpdateTime);
                _playerResources += _passiveIncome;

                ResourceUpdated?.Invoke(_playerResources);
            }
        }
    }
}