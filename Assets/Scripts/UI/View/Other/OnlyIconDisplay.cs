using JetBrains.Annotations;
using TMPro;
using UI.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.View.Other
{
    public class OnlyIconDisplay : UIInteractable
    {
        [SerializeField] private Image _image;
        [SerializeField] private ItemData _data;
        [SerializeField, CanBeNull] private TMP_Text _cost;

        private ItemDisplay _toDisplay;
        private bool _displayCost;
        
        public void Initialize(ItemDisplay display, bool displayCost)
        {
            _displayCost = displayCost; 
            if (!displayCost && _cost != null) _cost.gameObject.SetActive(false);

            _toDisplay = display;
        }

        public void Set(ItemData info)
        {
            _data = info;
            _image.sprite = info.Image;
            
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

        public override void OnPointerEnter(PointerEventData eventData) { }

        public override void OnPointerExit(PointerEventData eventData) { }

        public override void OnPointerClick(PointerEventData eventData)
        {
            _toDisplay.Set(_data);
        }
    }
}