using Player.Interactable;
using UnityEngine;

namespace Core.Behaviour.PlayerStateMachine
{
    public abstract class PlayerState
    {
        protected PlayerStateMachine StateMachine { get; }
        protected Transform Trigger { get; }

        protected PlayerState(PlayerStateMachine stateMachine, Transform trigger)
        {
            StateMachine = stateMachine;
            Trigger = trigger;
        }
        
        public abstract void PrimaryAction();
        public abstract void SecondaryAction();

        public abstract void InteractableTriggerEnter(ISceneInteractable other);
        public abstract void InteractableTriggerExit(ISceneInteractable other);
        
        public virtual void EnterState() {}
        public virtual void ExitState() {}
    }
}