using Core.Resource;
using UnityEngine;

namespace Core.Building.Types
{
    public class ResourceGatherer : Building
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _amount;
        [SerializeField] private float _gatherMultiplierPerLevel = 2f;

        public override void Build()
        {
            base.Build();
            ResourceManager.Instance.AddIncome(_type, _amount);
        }

        public override void Upgrade()
        {
            base.Upgrade();
            ResourceManager.Instance.AddIncome(_type, _amount);
            _amount = (int)(_amount * _gatherMultiplierPerLevel);
        }

        protected override void BeforeDestroy()
        {
            ResourceManager.Instance.AddIncome(_type, -_amount);
        }
    }
}