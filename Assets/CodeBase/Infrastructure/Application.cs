using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.StateMachine;

namespace CodeBase.Infrastructure
{
    public class Application
    {
        private readonly ServiceLocator _serviceLocator = new();
        public IStateMachine StateMachine { get; }

        public Application(ICoroutineRunner coroutineRunner) => 
            StateMachine = new ApplicationStateMachine(new SceneLoader(coroutineRunner), _serviceLocator);
    }
}