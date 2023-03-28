using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.Firebase;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using CodeBase.Logic.Loading;

namespace CodeBase.Infrastructure.StateMachine
{
    public class ApplicationStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public ApplicationStateMachine(SceneLoader sceneLoader, ServiceLocator serviceLocator,
            LoadingCurtain loadingCurtain)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
                [typeof(LoadSavedDataState)] = new LoadSavedDataState(this,
                    ServiceLocator.GetService<IPersistentSavedDataService>(),
                    ServiceLocator.GetService<ISaveLoadService>()),
                [typeof(ReadRemoteDataState)] = new ReadRemoteDataState(this, ServiceLocator.GetService<IFirebaseInitializer>(),
                    ServiceLocator.GetService<ISaveLoadService>(),
                    ServiceLocator.GetService<IPersistentSavedDataService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain),
                [typeof(PlugState)] = new PlugState(),
                [typeof(WebviewState)] = new WebviewState(this, ServiceLocator.GetService<IPersistentSavedDataService>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            IPayloadState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}