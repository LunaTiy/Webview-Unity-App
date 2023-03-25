namespace CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(IStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.Load("Initial", OnLoaded);

        private void OnLoaded() => 
            _stateMachine.Enter<LoadLevelState>();

        public void Exit() { }
    }

    public class LoadLevelState : IState
    {
        public void Enter()
        {
            
        }

        public void Exit() { }
    }
}