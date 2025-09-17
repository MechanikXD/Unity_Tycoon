using JetBrains.Annotations;
using TMPro;
using UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View.Other
{
    public class OnlyIconDisplay : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private bool _displayCost;
        [SerializeField, CanBeNull] private TMP_Text _cost;

        private void Awake()
        {
            if (!_displayCost && _cost != null) _cost.gameObject.SetActive(false);
        }

        public void Set(ItemData info)
        {
            _image.sprite = info.Image.sprite;
            
            if (_displayCost && _cost != null)
                _cost.SetText(TextFormatHelper.ResourceBundleToString(info.Cost));
            else if (!_displayCost && _cost != null)
                _cost.SetText(string.Empty);
        }

        public void Clear()
        {
            _image.sprite = null;
            if (_cost != null) _cost.SetText(string.Empty);
        }
    }
}