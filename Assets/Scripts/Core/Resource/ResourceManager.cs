using System.Collections;
using System.Collections.Generic;
using Core.Behaviour.SingletonBehaviour;
using UnityEngine;

namespace Core.Resource
{
    public class ResourceManager : SingletonBase<ResourceManager>
    {
        [SerializeField] private float _resourceUpdateTime;
        private Dictionary<ResourceType, int> _playerResources;
        private Dictionary<ResourceType, int> _passiveIncome;
        private Coroutine _resourceUpdateCoroutine;
        private bool _coroutineCancellationToken;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
            _resourceUpdateCoroutine = StartCoroutine(UpdateResourcesOnTimer());
        }
        
        public bool HasEnoughResources(ResourceBundle resources)
        {
            foreach (var tuple in resources.Resources)
            {
                if (_playerResources[tuple.type] < tuple.amount)
                {
                    return false;
                }
            }

            return true;
        }

        public void Spend(ResourceBundle resources)
        {
            if (!HasEnoughResources(resources)) return;

            foreach (var tuple in resources.Resources)
            {
                _playerResources[tuple.type] -= tuple.amount;
            }
        }

        public void AddIncome(ResourceType ofType, int increment)
        {
            _passiveIncome[ofType] += increment;
        }

        private void ProcessPassiveIncome()
        {
            foreach (var pair in _passiveIncome)
            {
                _playerResources[pair.Key] += pair.Value;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopCoroutine(_resourceUpdateCoroutine);
        }
        
        private void Initialize()
        {
            _playerResources = new Dictionary<ResourceType, int>();
            _passiveIncome = new Dictionary<ResourceType, int>
            {
                [ResourceType.Gold] = 5,
                [ResourceType.Wood] = 1,
                [ResourceType.Stone] = 1
            };
        }

        private IEnumerator UpdateResourcesOnTimer()
        {
            _coroutineCancellationToken = true;
            while (_coroutineCancellationToken)
            {
                yield return new WaitForSeconds(_resourceUpdateTime);
                ProcessPassiveIncome();
            }
        }
    }
}