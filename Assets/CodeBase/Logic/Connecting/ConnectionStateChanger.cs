using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;

namespace CodeBase.Logic.Connecting
{
    public class ConnectionStateChanger : MonoBehaviour
    {
        [SerializeField] private ConnectionChecker _checker;
        private IStateMachineProviderService _stateMachineProvider;

        public bool HasUrl { get; set; }

        private void Construct(IStateMachineProviderService stateMachineProvider) =>
            _stateMachineProvider = stateMachineProvider;

        private void Start() =>
            Construct(ServiceLocator.GetService<IStateMachineProviderService>());

        private void OnEnable() =>
            _checker.InternetConnected += InternetConnectedHandler;

        private void OnDisable() =>
            _checker.InternetConnected -= InternetConnectedHandler;

        private void InternetConnectedHandler()
        {
            if (HasUrl)
                _stateMachineProvider.StateMachine.Enter<LoadLevelState, string>(Constants.WebviewScene);
            else
                _stateMachineProvider.StateMachine.Enter<ReadRemoteDataState>();
        }
    }
}