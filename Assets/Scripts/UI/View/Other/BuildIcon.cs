using Core.Building;
using Core.Resource;
using Player.Interactable;
using Player.Interactable.States;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.View.Other
{
    public class BuildIcon : UIInteractable
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Building _building;
        [SerializeField] private Image _background;

        public void SetBackgroundColor(Color color)
        {
            _background.color = color;
        }
        
        public void Set(Sprite image, ResourceBundle cost, Building script)
        {
            _image.sprite = image;
            _cost.SetText(TextFormatHelper.ResourceBundleToString(cost));
            _building = script;
        }

        public void Clear()
        {
            _image.sprite = null;
            _cost.SetText(string.Empty);
            _building = null;
        }
        
        public override void OnPointerEnter(PointerEventData eventData) { }

        public override void OnPointerExit(PointerEventData eventData) { }

        public override void OnPointerClick(PointerEventData eventData)
        {
            BuildingState.CurrentlySelected = _building;
            InteractionTrigger.EnterState<BuildingState>();
        }
    }
}