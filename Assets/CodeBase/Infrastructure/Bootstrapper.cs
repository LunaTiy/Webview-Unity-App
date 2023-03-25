using System.Collections;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Application _application;
        
        private void Start()
        {
            _application = new Application(this);
            _application.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}