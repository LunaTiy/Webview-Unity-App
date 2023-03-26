using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.Firebase;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private readonly IStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(IStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;

            RegisterServices(serviceLocator);
        }

        public void Enter() =>
            _sceneLoader.Load(InitialScene, OnLoaded);

        public void Exit() { }

        private void OnLoaded() =>
            _stateMachine.Enter<LoadSavedDataState>();

        private static void RegisterServices(ServiceLocator serviceLocator)
        {
            serviceLocator.RegisterSingle<IPersistentSavedDataService>(new PersistentSavedDataService());
            serviceLocator.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(ServiceLocator.GetService<IPersistentSavedDataService>()));
            serviceLocator.RegisterSingle<IFirebaseInitializer>(new FirebaseInitializer());
        }
    }
}