using System;
using System.Collections.Generic;

namespace Core.Resource
{
    [Serializable]
    public struct ResourceBundle
    {
        public int Money { get; set; }
        public int Wood { get; set; }
        public int Stone { get; set; }
        public int Ore { get; set; }
        public int People { get; set; }

        public List<(ResourceType type, int amount)> Resources =>
            new() {
                (ResourceType.Gold, Money),
                (ResourceType.Wood, Wood),
                (ResourceType.Stone, Stone),
                (ResourceType.Ore, Ore),
                (ResourceType.People, People),
            };
    }
}