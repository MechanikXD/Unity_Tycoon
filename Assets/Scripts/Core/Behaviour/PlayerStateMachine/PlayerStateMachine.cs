using System;
using System.Collections.Generic;
using Player.Interactable;

namespace Core.Behaviour.PlayerStateMachine
{
    public class PlayerStateMachine
    {
        private Dictionary<Type, PlayerState> _playerStates;
        private PlayerState _currentPlayerState;

        public void OnPrimary() => _currentPlayerState.PrimaryAction();
        public void OnSecondary() => _currentPlayerState.SecondaryAction();

        public void OnInteractableTriggerEnter(ISceneInteractable other) =>
            _currentPlayerState.InteractableTriggerEnter(other);
        public void OnInteractableTriggerExit(ISceneInteractable other) =>
            _currentPlayerState.InteractableTriggerExit(other);

        public void ChangeState<T>() where T : PlayerState
        {
            _currentPlayerState.ExitState();
            _currentPlayerState = _playerStates[typeof(T)];
            _currentPlayerState.EnterState();
        }

        public void Initialize(PlayerState startingState)
        {
            _playerStates = new Dictionary<Type, PlayerState>();
            AddState(startingState);
            _currentPlayerState = startingState;
            _currentPlayerState.EnterState();
        }

        public void AddState(PlayerState state)
        {
            _playerStates.TryAdd(state.GetType(), state);
        }
    }
}