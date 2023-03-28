using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using CodeBase.Webview;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class WebviewState : IState
    {
        private const string WebviewProvider = "WebviewProvider";
        
        private readonly ApplicationStateMachine _stateMachine;
        private readonly IPersistentSavedDataService _savedDataService;

        public WebviewState(ApplicationStateMachine stateMachine, IPersistentSavedDataService savedDataService)
        {
            _stateMachine = stateMachine;
            _savedDataService = savedDataService;
        }

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
            webviewProvider.Url = _savedDataService.SavedData.url;
            webviewProvider.enabled = true;
        }
    }
}