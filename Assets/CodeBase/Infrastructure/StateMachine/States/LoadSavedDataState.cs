using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;

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
            _persistentSavedDataService.SavedData = _saveLoadService.Load() ?? new SavedData();

            if (string.IsNullOrEmpty(_persistentSavedDataService.SavedData.url))
                _stateMachine.Enter<ReadRemoteDataState>();
            else
                _stateMachine.Enter<LoadLevelState>();
        }

        public void Exit() { }
    }
}