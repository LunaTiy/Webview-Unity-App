using CodeBase.Infrastructure.StateMachine;

namespace CodeBase.Infrastructure
{
    public class Application
    {
        public IStateMachine StateMachine { get; }

        public Application(ICoroutineRunner coroutineRunner) => 
            StateMachine = new ApplicationStateMachine(new SceneLoader(coroutineRunner));
    }
}