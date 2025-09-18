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
        [Space]
        [SerializeField] private Color _allowBuildColor = new Color(.4f, .8f, 1f, .4f);
        [SerializeField] private Color _prohibitBuildColor = new Color(1f, .4f, .4f, .4f);

        public void Set(Image image, ResourceBundle cost, Building script)
        {
            _image.sprite = image.sprite;
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