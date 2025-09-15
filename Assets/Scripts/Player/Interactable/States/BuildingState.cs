using Core.Behaviour.PlayerStateMachine;
using Core.Building;
using UnityEngine;

namespace Player.Interactable.States
{
    public class BuildingState : PlayerState
    {
        public static Building CurrentlySelected { private get; set; }
        private Building _ghostBuilding;

        public BuildingState(PlayerStateMachine sm, Transform trigger) : base(sm, trigger) { }

        public override void EnterState()
        {
            _ghostBuilding = Object.Instantiate(CurrentlySelected, Trigger);

            var ghostTransform = _ghostBuilding.transform;
            var position = ghostTransform.localPosition;
            position.y = CurrentlySelected.HalfHeight;
            ghostTransform.localPosition = position;
        }

        public override void PrimaryAction()
        {
            if (!_ghostBuilding.CanBePlaced) return;

            // TODO: BAD. Use object pooling
            var newInstance = Object.Instantiate(_ghostBuilding, _ghostBuilding.transform.position, Quaternion.identity);
            newInstance.OnBuild();
        }

        public override void SecondaryAction()
        {
            StateMachine.ChangeState<NormalState>();
        }

        public override void ExitState()
        {
            Object.Destroy(_ghostBuilding.gameObject);
        }

        public override void InteractableTriggerEnter(ISceneInteractable other) { }

        public override void InteractableTriggerExit(ISceneInteractable other) { }
    }
}