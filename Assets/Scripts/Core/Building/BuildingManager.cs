using System;
using System.Collections.Generic;
using Core.Behaviour.SingletonBehaviour;
using UnityEngine;

namespace Core.Building
{
    public class BuildingManager : SingletonBase<BuildingManager>
    {
        private LinkedList<Building> _buildings;
        private HashSet<Type> _singletons;

        protected override void Awake()
        {
            base.Awake();
            _buildings = new LinkedList<Building>();
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
                
            _buildings.AddLast(building);
        }

        public bool HasSingleton(Type building)
        {
            return _singletons.Contains(building);
        }
    }
}