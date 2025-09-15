using Core.Behaviour.PlayerStateMachine;
using JetBrains.Annotations;
using UnityEngine;

namespace Player.Interactable.States
{
    public class NormalState : PlayerState
    {
        [CanBeNull] private ISceneInteractable _currentlySelected;
        
        public NormalState(PlayerStateMachine sm, Transform trigger) : base(sm, trigger) { }
        
        public override void PrimaryAction()
        {
            _currentlySelected?.PrimaryAction();
        }

        public override void SecondaryAction()
        {
            _currentlySelected?.SecondaryAction();
        }

        public override void InteractableTriggerEnter(ISceneInteractable other)
        {
            _currentlySelected = other;
        }

        public override void InteractableTriggerExit(ISceneInteractable other)
        {
            if (_currentlySelected != other) return;
                
            _currentlySelected = null;
        }
    }
}