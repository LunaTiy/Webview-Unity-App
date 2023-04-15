using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadSavedDataState : IState
    {
        private readonly ApplicationStateMachine _stateMachine;
        private readonly IPersistentSavedDataService _persistentSavedDataService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadSavedDataState(ApplicationStateMachine stateMachine,
            IPersistentSavedDataService persistentSavedDataService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _persistentSavedDataService = persistentSavedDataService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            Debug.Log("Enter Load saved data state");
            
            _persistentSavedDataService.SavedData = _saveLoadService.Load() ?? new SavedData();

            if (string.IsNullOrEmpty(_persistentSavedDataService.SavedData.url))
                _stateMachine.Enter<CheckDeviceState>();
            else if (UnityEngine.Application.internetReachability != NetworkReachability.NotReachable)
                _stateMachine.Enter<LoadLevelState, string>(Constants.WebviewScene);
            else
                _stateMachine.Enter<DisconnectState, bool>(true);
        }

        public void Exit() { }
    }
}