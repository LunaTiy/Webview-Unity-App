using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Di;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StateMachine.States;

namespace CodeBase.Infrastructure.StateMachine
{
    public class ApplicationStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public ApplicationStateMachine(SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
                [typeof(LoadSavedDataState)] = new LoadSavedDataState(this,
                    ServiceLocator.GetService<IPersistentSavedDataService>(),
                    ServiceLocator.GetService<ISaveLoadService>()),
                [typeof(ReadRemoteDataState)] = new ReadRemoteDataState(),
                [typeof(LoadLevelState)] = new LoadLevelState(),
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