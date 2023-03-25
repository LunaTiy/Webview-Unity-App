using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StateMachine.States;

namespace CodeBase.Infrastructure.StateMachine
{
    public class ApplicationStateMachine : IStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public ApplicationStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();

            IState state = _states[typeof(TState)];
            state.Enter();

            _activeState = state;
        }
    }
}