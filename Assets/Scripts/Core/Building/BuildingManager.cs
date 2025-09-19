using System;
using System.Collections.Generic;
using Core.Behaviour.Singleton;
using Core.DataSave;
using UI.Data;
using UnityEngine;

namespace Core.Building
{
    public class BuildingManager : SingletonBase<BuildingManager>, ISaveAble
    {
        private List<Building> _buildings;
        private HashSet<Type> _singletons;

        protected override void Awake()
        {
            base.Awake();
            SaveManager.Register("Building Manager", this);
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

        public object SaveData()
        {
            var building = new BuildingSaveData[_buildings.Count];

            for (var i = 0; i < _buildings.Count; i++)
            {
                building[i] = new BuildingSaveData(_buildings[i].PrefabIdInDataSet,
                    _buildings[i].transform.position, _buildings[i].CurrentUpgradeLevel);
            }

            return new BuildingSaveDataArray(building);
        }

        public void LoadData(object data)
        {
            var dataSet = Resources.Load("Building DataSet") as BuildingDataSet;
            if (dataSet == null) return;
            
            foreach (var building in ((BuildingSaveDataArray)data).Array)
            {
                var instance = Instantiate(dataSet.DataSet[building.BuildingID].Prefab, building.BuildingPosition, Quaternion.identity);
                for (var i = 0; i < building.UpgradeLevel; i++)
                {
                    instance.Upgrade();
                }
            }
        }
    }

    [Serializable]
    public class BuildingSaveDataArray
    {
        [SerializeField] private BuildingSaveData[] _array;

        public BuildingSaveData[] Array => _array;

        public BuildingSaveDataArray(BuildingSaveData[] array)
        {
            _array = array;
        }
    }

    [Serializable]
    public class BuildingSaveData
    {
        [SerializeField] private int _buildingID;
        [SerializeField] private Vector3 _buildingPosition;
        [SerializeField] private int _upgradeLevel;

        public int BuildingID => _buildingID;
        public Vector3 BuildingPosition => _buildingPosition;
        public int UpgradeLevel => _upgradeLevel;

        public BuildingSaveData(int id, Vector3 pos, int level)
        {
            _buildingID = id;
            _buildingPosition = pos;
            _upgradeLevel = level;
        }
    }
}