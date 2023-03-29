using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Firebase
{
    public class FirebaseInitializer : IFirebaseInitializer
    {
        private DependencyStatus _status = DependencyStatus.UnavailableDisabled;
        private bool _initialized;

        private static FirebaseRemoteConfig FirebaseConfig => FirebaseRemoteConfig.DefaultInstance;

        public async Task Initialize()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                _status = task.Result;
                
                if (_status == DependencyStatus.Available)
                {
                    InitializeFirebase();
                    _initialized = true;
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + _status);
                }
            });
        }

        public bool TryGetUrl(out string url)
        {
            url = string.Empty;

            if (!_initialized)
            {
                Debug.Log("Firebase not initialized or fetched");
                return false;
            }

            url = FirebaseConfig.GetValue(Constants.RemoteUrlKey).StringValue;

            return !string.IsNullOrEmpty(url);
        }

        private static async Task InitializeFirebase()
        {
            var defaults = new Dictionary<string, object> { { "url", "" } };

            await FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
                .ContinueWithOnMainThread(_ =>
                {
                    Debug.Log("RemoteConfig configured and ready!");
                    FetchDataAsync();
                });
        }

        private static async Task FetchDataAsync()
        {
            Debug.Log("Fetching data...");
            Task fetchTask =
                FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                    TimeSpan.Zero);

            await fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private static void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
                Debug.Log("Fetch canceled.");
            else if (fetchTask.IsFaulted)
                Debug.Log("Fetch encountered an error.");
            else if (fetchTask.IsCompleted)
                Debug.Log("Fetch completed successfully!");

            var info = FirebaseRemoteConfig.DefaultInstance.Info;
            
            switch (info.LastFetchStatus)
            {
                case LastFetchStatus.Success:
                    FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                        .ContinueWithOnMainThread(task =>
                            Debug.Log($"Remote data loaded and ready (last fetch time {info.FetchTime})."));
                    break;
                
                case LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case FetchFailureReason.Error:
                            Debug.Log("Fetch failed for unknown reason");
                            break;
                        case FetchFailureReason.Throttled:
                            Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                            break;
                        case FetchFailureReason.Invalid:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case LastFetchStatus.Pending:
                    Debug.Log("Latest Fetch call still pending.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}