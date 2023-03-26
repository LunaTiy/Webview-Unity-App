using System.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Firebase
{
    public class FirebaseInitializer : IFirebaseInitializer
    {
        private DependencyStatus _status = DependencyStatus.UnavailableDisabled;
        private bool _initialized;
        private bool _fetched;

        private FirebaseRemoteConfig FirebaseRemoteConfig { get; set; }
        private bool InitializedAndFetched => _initialized && _fetched;
        
        public async Task Initialize()
        {
            if (InitializedAndFetched)
                return;
            
            _status = FirebaseApp.CheckDependenciesAsync().Result;

            if (!TryFixDependencies())
                return;

            await FetchAndActiveRemoteConfig();

            _initialized = true;
        }

        public bool TryGetUrl(out string url)
        {
            url = string.Empty;
            
            if (!InitializedAndFetched)
            {
                Debug.Log("Firebase isn't initialized or fetched");
                return false;
            }

            url = FirebaseRemoteConfig.GetValue(Constants.RemoteUrlKey).StringValue;
            
            return !string.IsNullOrEmpty(url);
        }

        private bool TryFixDependencies()
        {
            if (_status == DependencyStatus.Available)
                return true;

            _status = FirebaseApp.CheckAndFixDependenciesAsync().Result;

            if (_status == DependencyStatus.Available)
                return true;

            Debug.Log("Firebase isn't available");
            return false;
        }

        private async Task FetchAndActiveRemoteConfig()
        {
            FirebaseRemoteConfig = FirebaseRemoteConfig.DefaultInstance;
            
            // TODO: clear prev values
            
            await FirebaseRemoteConfig.FetchAndActivateAsync().ContinueWith(OnActiveRemoteConfig);
        }

        private void OnActiveRemoteConfig(Task<bool> task)
        {
            ConfigInfo info = FirebaseRemoteConfig.Info;

            if (task.IsCompleted && info.LastFetchStatus == LastFetchStatus.Success)
                _fetched = true;
        }
    }
}