using CodeBase.Infrastructure.Services.Firebase;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class ReadRemoteDataState : IState
    {
        private readonly IFirebaseInitializer _firebaseInitializer;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentSavedDataService _persistentSavedDataService;

        public ReadRemoteDataState(IFirebaseInitializer firebaseInitializer, ISaveLoadService saveLoadService,
            IPersistentSavedDataService persistentSavedDataService)
        {
            _firebaseInitializer = firebaseInitializer;
            _saveLoadService = saveLoadService;
            _persistentSavedDataService = persistentSavedDataService;
        }

        public async void Enter()
        {
            await _firebaseInitializer.Initialize();
            LoadNext();
        }

        private void LoadNext()
        {
            if (!_firebaseInitializer.TryGetUrl(out string url))
            {
                // TODO: Load plug
                Debug.Log("Load plug");
            }
            else
            {
                SaveUrl(url);
                Debug.Log($"Load webview: {url}");
                // TODO: Load webview
            }
        }

        public void Exit() { }

        private void SaveUrl(string url)
        {
            _persistentSavedDataService.SavedData.url = url;
            _saveLoadService.Save();
        }
    }
}