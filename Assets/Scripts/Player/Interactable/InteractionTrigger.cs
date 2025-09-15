using Core.Behaviour.PlayerStateMachine;
using Player.Interactable.States;
using UnityEngine;

namespace Player.Interactable
{
    public class InteractionTrigger : MonoBehaviour
    {
        private static PlayerStateMachine _stateMachine;

        private void Awake()
        {
            InitializeStateMachine();
        }

        public static void EnterBuildingMode()
        {
            _stateMachine.ChangeState<BuildingState>();
        }

        public void PrimaryAction()
        {
            _stateMachine.OnPrimary();
        }

        public void SecondaryAction()
        {
            _stateMachine.OnSecondary();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ISceneInteractable>(out var interactable))
            {
                _stateMachine.OnInteractableTriggerEnter(interactable);    
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ISceneInteractable>(out var interactable))
            {
                _stateMachine.OnInteractableTriggerExit(interactable);
            }
        }
        
        private void InitializeStateMachine()
        {
            _stateMachine = new PlayerStateMachine();
            _stateMachine.Initialize(new NormalState(_stateMachine, transform));
            
            _stateMachine.AddState(new BuildingState(_stateMachine, transform));
        }
    }
}