using System;
using Core.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Data
{
    [CreateAssetMenu(fileName = "Item DataSet", menuName = "ScriptableObjects/Item DataSet")]
    public class ItemDataSet : ScriptableObject
    {
        [SerializeField] private ItemData[] _dataset;

        public ItemData[] DataSet => _dataset;
    }

    [Serializable]
    public abstract class ItemData
    {
        [SerializeField] private Image _image;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private ResourceBundle _cost;
        
        public Image Image => _image;
        public string Title => _title;
        public string Description => _description;
        public ResourceBundle Cost => _cost;
        
        public abstract void OnItemOwned();
        public abstract void OnDisable();
    }
}