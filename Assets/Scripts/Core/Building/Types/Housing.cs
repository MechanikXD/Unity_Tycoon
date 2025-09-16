using Core.Resource;
using UnityEngine;

namespace Core.Building.Types
{
    public class Housing : Building
    {
        [SerializeField] private int _populationIncrease;
        [SerializeField] private int _populationIncreasePerLevel;

        public override bool CanBeDestroyed => ResourceManager.Instance.MaxPopulation -
            ResourceManager.Instance.WorkingPopulation - _populationIncrease >= 0;

        public override void Build()
        {
            base.Build();
            ResourceManager.Instance.AddMaxPopulation(_populationIncrease);
        }

        public override void Upgrade()
        {
            base.Upgrade();
            ResourceManager.Instance.AddMaxPopulation(_populationIncreasePerLevel);
            _populationIncrease += _populationIncreasePerLevel;
        }

        protected override void BeforeDestroy()
        {
            ResourceManager.Instance.AddMaxPopulation(-_populationIncrease);
        }
    }
}