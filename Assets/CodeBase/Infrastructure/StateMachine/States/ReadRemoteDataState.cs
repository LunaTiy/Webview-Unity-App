using CodeBase.Infrastructure.Services.Firebase;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class ReadRemoteDataState : IState
    {
        private readonly ApplicationStateMachine _stateMachine;
        private readonly IFirebaseInitializer _firebaseInitializer;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentSavedDataService _persistentSavedDataService;

        public ReadRemoteDataState(ApplicationStateMachine stateMachine, IFirebaseInitializer firebaseInitializer, ISaveLoadService saveLoadService,
            IPersistentSavedDataService persistentSavedDataService)
        {
            _stateMachine = stateMachine;
            _firebaseInitializer = firebaseInitializer;
            _saveLoadService = saveLoadService;
            _persistentSavedDataService = persistentSavedDataService;
        }

        public async void Enter()
        {
            await _firebaseInitializer.Initialize();
            LoadNext();
        }

        public void Exit() { }

        private void LoadNext()
        {
            string nextScene = GetNextScene();
            
            _stateMachine.Enter<LoadLevelState, string>(nextScene);
        }

        private string GetNextScene()
        {
            string nextScene;
            
            if (!_firebaseInitializer.TryGetUrl(out string url))
            {
                nextScene = Constants.PlugScene;
            }
            else
            {
                SaveUrl(url);
                nextScene = Constants.WebviewScene;
            }

            return nextScene;
        }

        private void SaveUrl(string url)
        {
            _persistentSavedDataService.SavedData.url = url;
            _saveLoadService.Save();
        }
    }
}