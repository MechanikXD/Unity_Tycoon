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
        [SerializeField] private Image _icon;
        [SerializeField] private ResourceBundle _cost;
        [SerializeField] private Building _prefab;

        public Image Icon => _icon;
        public ResourceBundle Cost => _cost;
        public Building Prefab => _prefab;
    }
}