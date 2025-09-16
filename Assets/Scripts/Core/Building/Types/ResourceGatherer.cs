using Core.Resource;
using UnityEngine;

namespace Core.Building.Types
{
    public class ResourceGatherer : Building
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _amount;
        [SerializeField] private float _gatherIncreasePerLevel;

        public override void Build()
        {
            base.Build();
            ResourceManager.Instance.AddIncome(_type, _amount);
        }

        public override void RepositionStart()
        {
            base.RepositionStart();
            ResourceManager.Instance.AddIncome(_type, -_amount);
        }

        public override void RepositionEnd()
        {
            base.RepositionStart();
            ResourceManager.Instance.AddIncome(_type, _amount);
        }

        public override void Upgrade()
        {
            base.Upgrade();
            ResourceManager.Instance.AddIncome(_type, _amount);
            _amount = (int)(_amount * _gatherIncreasePerLevel);
        }

        public override void Destroy()
        {
            base.Destroy();
            ResourceManager.Instance.AddIncome(_type, -_amount);
        }
    }
}