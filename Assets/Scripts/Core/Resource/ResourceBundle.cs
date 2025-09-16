using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Resource
{
    [Serializable]
    public struct ResourceBundle
    {
        [SerializeField] private int _gold;
        [SerializeField] private int _wood;
        [SerializeField] private int _stone;
        [SerializeField] private int _ore;
        [SerializeField] private int _people;
        
        public int Gold { get => _gold; set => _gold = value; }
        public int Wood { get => _wood; set => _wood = value; }
        public int Stone { get => _stone; set => _stone = value; }
        public int Ore { get => _ore; set => _ore = value; }
        
        public int People { get => _people; set => _people = value; }

        public List<(ResourceType type, int amount)> Resources =>
            new() {
                (ResourceType.Gold, Gold),
                (ResourceType.Wood, Wood),
                (ResourceType.Stone, Stone),
                (ResourceType.Ore, Ore)
            };

        public static ResourceBundle operator +(ResourceBundle current, ResourceBundle other)
        {
            return new ResourceBundle
            {
                Gold = current.Gold + other.Gold,
                Wood = current.Wood + other.Wood,
                Stone = current.Stone + other.Stone,
                Ore = current.Ore + other.Ore,
                People = current.People + other.People,
            };
        }
        
        public static ResourceBundle operator -(ResourceBundle current, ResourceBundle other)
        {
            return new ResourceBundle
            {
                Gold = current.Gold - other.Gold,
                Wood = current.Wood - other.Wood,
                Stone = current.Stone - other.Stone,
                Ore = current.Ore - other.Ore,
                People = current.People - other.People,
            };
        }
    }
}