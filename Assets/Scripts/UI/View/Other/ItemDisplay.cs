using JetBrains.Annotations;
using TMPro;
using UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.Other
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private bool _displayCost;
        [SerializeField, CanBeNull] private TMP_Text _cost;

        private void Awake()
        {
            if (!_displayCost && _cost != null) _cost.gameObject.SetActive(false);
        }
        
        public void Set(ItemData info)
        {
            _image.sprite = info.Image.sprite;
            _title.SetText(info.Title);
            _description.SetText(info.Description);
            
            if (_displayCost && _cost != null)
                _cost.SetText(TextFormatHelper.ResourceBundleToString(info.Cost));
            else if (!_displayCost && _cost != null)
                _cost.SetText(string.Empty);
        }

        public void Clear()
        {
            _image.sprite = null;
            _title.SetText(string.Empty);
            _description.SetText(string.Empty);
            if (_cost != null) _cost.SetText(string.Empty);
        }
    }
}