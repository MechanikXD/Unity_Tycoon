using System;
using System.Collections.Generic;
using Core.Behaviour.Singleton;
using UnityEngine;

namespace Core.Building
{
    public class BuildingManager : SingletonBase<BuildingManager>
    {
        private List<Building> _buildings;
        private HashSet<Type> _singletons;

        protected override void Awake()
        {
            base.Awake();
            _buildings = new List<Building>();
            _singletons = new HashSet<Type>();
        }

        public void Add(Building building)
        {
            if (building.IsSingleton)
            {
                if (!_singletons.Add(building.GetType()))
                {
                    Debug.LogError("Two singleton were placed!");
                    return;
                }
            }
                
            _buildings.Add(building);
        }

        public void Remove(Building building)
        {
            if (building.IsSingleton)
            {
                _singletons.Remove(building.GetType());
            }
                
            _buildings.Remove(building);
        }
        
        public bool HasSingleton(Type building)
        {
            return _singletons.Contains(building);
        }
    }
}