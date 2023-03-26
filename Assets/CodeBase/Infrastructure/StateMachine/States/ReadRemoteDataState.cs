using CodeBase.Infrastructure.Services.Firebase;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class ReadRemoteDataState : IState
    {
        private readonly IFirebaseProvider _firebaseProvider;

        public ReadRemoteDataState(IFirebaseProvider firebaseProvider)
        {
            _firebaseProvider = firebaseProvider;
        }
        
        public void Enter()
        {
            _firebaseProvider.InitializeFirebase();

            if (!_firebaseProvider.TryGetUrl(out string url))
            {
                // TODO: Load plug
            }
            else
            {
                // TODO: Load webview
            }
        }

        public void Exit() { }
    }
}