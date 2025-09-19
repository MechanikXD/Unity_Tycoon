using JetBrains.Annotations;
using TMPro;
using UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.Other
{
    public class ItemDisplay : MonoBehaviour
    {
        private ItemData _currentDisplayed;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField, CanBeNull] private TMP_Text _cost;
        [SerializeField] private Sprite _clearImage;

        private bool _displayCost;
        public ItemData Current => _currentDisplayed;

        public void Initialize(bool displayCost)
        {
            if (!_displayCost && _cost != null) _cost.gameObject.SetActive(false);
        }
        
        public void Set(ItemData info)
        {
            _currentDisplayed = info;
            _image.sprite = info.Image;
            _title.SetText(info.Title);
            _description.SetText(info.Description);
            
            if (_displayCost && _cost != null)
                _cost.SetText(TextFormatHelper.ResourceBundleToString(info.Cost));
            else if (!_displayCost && _cost != null)
                _cost.SetText(string.Empty);
        }

        public void Clear()
        {
            _currentDisplayed = null;
            _image.sprite = _clearImage;
            _title.SetText(string.Empty);
            _description.SetText(string.Empty);
            if (_cost != null) _cost.SetText(string.Empty);
        }
    }
}