using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.Logic.Loading;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;
        private Application _application;
        
        private void Start()
        {
            _application = new Application(this, Instantiate(_loadingCurtainPrefab));
            _application.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}