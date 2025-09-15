using Core.Building;
using Player.Interactable;
using Player.Interactable.States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.BuildingTab
{
    public class BuildIcon : UIInteractable
    {
        [SerializeField] private Building _building;
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log($"About to select {gameObject.name}");
        }

        public override void OnPointerExit(PointerEventData eventData) { }

        public override void OnPointerClick(PointerEventData eventData)
        {
            BuildingState.CurrentlySelected = _building;
            InteractionTrigger.EnterBuildingMode();
        }
    }
}