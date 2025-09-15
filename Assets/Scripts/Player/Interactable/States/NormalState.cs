using Core.Behaviour.PlayerStateMachine;
using JetBrains.Annotations;
using UI;
using UI.HUD.View;
using UnityEngine;

namespace Player.Interactable.States
{
    public class NormalState : PlayerState
    {
        [CanBeNull] private ISceneInteractable _currentlySelected;
        
        public NormalState(PlayerStateMachine sm, Transform trigger) : base(sm, trigger) { }

        public override void ExitState()
        {
            UIManager.Instance.ExitHudCanvas<BuildInteractionView>();
        }

        public override void PrimaryAction()
        {
            _currentlySelected?.PrimaryAction();
        }

        public override void SecondaryAction()
        {
            if (_currentlySelected != null)
            {
                _currentlySelected.SecondaryAction();
            }
            else
            {
                UIManager.Instance.ExitHudCanvas<BuildInteractionView>();
            }
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