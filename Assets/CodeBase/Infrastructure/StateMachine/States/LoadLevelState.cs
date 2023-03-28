using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using CodeBase.Logic.Loading;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ApplicationStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        private string _nextScene;

        public LoadLevelState(ApplicationStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string nextScene)
        {
            _loadingCurtain.Show();

            _nextScene = nextScene;
            _sceneLoader.Load(_nextScene, OnLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            if (_nextScene == Constants.WebviewScene)
                _stateMachine.Enter<WebviewState>();
            else
                _stateMachine.Enter<PlugState>();
        }
    }
}