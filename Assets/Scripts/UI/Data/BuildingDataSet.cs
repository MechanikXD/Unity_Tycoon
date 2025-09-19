using System;
using Core.Building;
using Core.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Data
{
    [CreateAssetMenu(fileName = "Building DataSet", menuName = "ScriptableObjects/Building DataSet")]
    public class BuildingDataSet : ScriptableObject
    {
        [SerializeField] private BuildingData[] _dataset;

        public BuildingData[] DataSet => _dataset;
    }

    [Serializable]
    public class BuildingData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private Building _prefab;

        public Sprite Icon => _icon;
        public ResourceBundle Cost => _prefab.Cost;
        public Building Prefab => _prefab;
    }
}