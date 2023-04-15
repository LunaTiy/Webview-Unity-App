using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Firebase;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using CodeBase.Logic.Loading;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(IStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;

            RegisterServices(serviceLocator);
        }

        public void Enter() =>
            _sceneLoader.Load(Constants.InitialScene, OnLoaded);

        public void Exit() { }

        private void OnLoaded() =>
            _stateMachine.Enter<LoadSavedDataState>();

        private void RegisterServices(ServiceLocator serviceLocator)
        {
            serviceLocator.RegisterSingle<IPersistentSavedDataService>(new PersistentSavedDataService());
            serviceLocator.RegisterSingle<IStateMachineProviderService>(new StateMachineProviderService(_stateMachine));
            serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
            serviceLocator.RegisterSingle<IPlugFactory>(new PlugFactory(
                ServiceLocator.GetService<IAssetProvider>()));
            serviceLocator.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(ServiceLocator.GetService<IPersistentSavedDataService>()));
            serviceLocator.RegisterSingle<IFirebaseInitializer>(new FirebaseInitializer());
        }
    }
}