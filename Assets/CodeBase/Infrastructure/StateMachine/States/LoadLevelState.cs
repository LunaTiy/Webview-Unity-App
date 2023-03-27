using CodeBase.Infrastructure.StateMachine.States.Interfaces;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ApplicationStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private string _nextScene;

        public LoadLevelState(ApplicationStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter(string nextScene)
        {
            _nextScene = nextScene;
            _sceneLoader.Load(_nextScene, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            if (_nextScene == Constants.WebviewScene)
                _stateMachine.Enter<WebviewState>();
            else
                _stateMachine.Enter<PlugState>();
        }
    }
}