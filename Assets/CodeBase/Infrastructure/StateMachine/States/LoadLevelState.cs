using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using CodeBase.Logic.Loading;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ApplicationStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IPlugFactory _plugFactory;
        private readonly IPersistentSavedDataService _persistentSavedDataService;

        private string _nextScene;

        public LoadLevelState(ApplicationStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IPlugFactory plugFactory,
            IPersistentSavedDataService persistentSavedDataService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _plugFactory = plugFactory;
            _persistentSavedDataService = persistentSavedDataService;
        }

        public void Enter(string nextScene)
        {
            Debug.Log($"Loading scene: {nextScene}");
            
            _loadingCurtain.Show();

            _nextScene = nextScene;
            _sceneLoader.Load(_nextScene, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            if (_nextScene == Constants.PlugScene)
            {
                _plugFactory.CreatePlug();
                InformProgressReaders();

                _stateMachine.Enter<PlugState>();
            }
            else
            {
                _stateMachine.Enter<WebviewState>();
            }
        }

        private void InformProgressReaders()
        {
            foreach (ISaveProgressReader progressReader in _plugFactory.SaveProgressReaders)
                progressReader.Load(_persistentSavedDataService.SavedData);
        }
    }
}