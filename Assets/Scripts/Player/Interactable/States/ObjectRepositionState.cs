using Core.Behaviour.PlayerStateMachine;
using Core.Building;
using UnityEngine;

namespace Player.Interactable.States
{
    public class ObjectRepositionState : PlayerState
    {
        private static Vector3 _originalPosition;
        private static Building _objectToMove;
        
        public ObjectRepositionState(PlayerStateMachine sm, Transform trigger) : base(sm, trigger) { }

        public static void SetObjectToMove(Building building)
        {
            _originalPosition = building.transform.position;
            _objectToMove = building;
        }

        public override void EnterState()
        {
            _objectToMove.OnRepositionStart();
            
            var transform = _objectToMove.transform;
            transform.SetParent(Trigger.transform);
            
            var newPosition = Vector3.zero;
            newPosition.y += _objectToMove.HalfHeight;
            transform.localPosition = newPosition;
        }

        public override void PrimaryAction()
        {
            if (!_objectToMove.CanBePlaced) return;
            
            // Object will be unparented and keep it's new position
            StateMachine.ChangeState<NormalState>();
        }


        public override void SecondaryAction()
        {
            _objectToMove.transform.position = _originalPosition;
            StateMachine.ChangeState<NormalState>();
        }

        public override void ExitState()
        {
            _objectToMove.transform.parent = null;
            _objectToMove.OnRepositionEnd();
        }

        public override void InteractableTriggerEnter(ISceneInteractable other) { }

        public override void InteractableTriggerExit(ISceneInteractable other) { }
    }
}