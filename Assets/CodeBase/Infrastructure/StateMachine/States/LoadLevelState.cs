using CodeBase.Infrastructure.StateMachine.States.Interfaces;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ApplicationStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(ApplicationStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter(string nextScene)
        {
            _sceneLoader.Load(nextScene);
            EnterNextState(nextScene);
        }

        private void EnterNextState(string nextScene)
        {
            if (nextScene == Constants.PlugScene)
                _stateMachine.Enter<PlugState>();
            else
                _stateMachine.Enter<WebviewState>();
        }

        public void Exit() { }
    }
}