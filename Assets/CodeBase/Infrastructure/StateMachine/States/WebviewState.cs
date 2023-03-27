using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using CodeBase.Webview;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class WebviewState : IState
    {
        private const string WebviewProvider = "WebviewProvider";
        
        private readonly ApplicationStateMachine _stateMachine;

        public WebviewState(ApplicationStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enter()
        {
            if (!TryFindProvider(out WebviewProvider webviewProvider))
            {
                LoadPlug();
                return;
            }

            InitializeWebview(webviewProvider);
        }

        public void Exit() { }

        private static bool TryFindProvider(out WebviewProvider webviewProvider)
        {
            webviewProvider = null;
            GameObject webviewProviderObject = GameObject.FindWithTag(WebviewProvider);

            return webviewProviderObject != null && webviewProviderObject.TryGetComponent(out webviewProvider);
        }

        private void LoadPlug() => 
            _stateMachine.Enter<LoadLevelState, string>(Constants.PlugScene);

        private void InitializeWebview(WebviewProvider webviewProvider)
        {
            // TODO: Get url and enable webviewProvider
        }
    }
}