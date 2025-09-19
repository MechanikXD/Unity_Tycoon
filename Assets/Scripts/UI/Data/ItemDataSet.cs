using System;
using Core.Items;
using Core.Resource;
using UnityEngine;

namespace UI.Data
{
    [CreateAssetMenu(fileName = "Item DataSet", menuName = "ScriptableObjects/Item DataSet")]
    public class ItemDataSet : ScriptableObject
    {
        [SerializeField] private ItemData[] _dataset;

        public ItemData[] DataSet => _dataset;
    }

    [Serializable]
    public class ItemData
    {
        [SerializeField] private Sprite _image;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private ResourceBundle _cost;
        [SerializeReference] private ItemActionBase _action;
        public bool IsOwned { get; set; } 

        public ItemActionBase Action => _action;
        public Sprite Image => _image;
        public string Title => _title;
        public string Description => _description;
        public ResourceBundle Cost => _cost;
    }
}