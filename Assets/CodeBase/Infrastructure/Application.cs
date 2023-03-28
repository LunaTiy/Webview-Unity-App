using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Logic.Loading;

namespace CodeBase.Infrastructure
{
    public class Application
    {
        private readonly ServiceLocator _serviceLocator = new();
        public IStateMachine StateMachine { get; }

        public Application(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain) => 
            StateMachine = new ApplicationStateMachine(new SceneLoader(coroutineRunner), _serviceLocator, loadingCurtain);
    }
}