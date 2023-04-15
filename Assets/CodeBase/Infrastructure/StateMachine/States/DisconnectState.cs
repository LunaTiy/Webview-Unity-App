using CodeBase.AssetManagement;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using CodeBase.Logic.Connecting;
using CodeBase.Logic.Loading;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class DisconnectState : IPayloadState<bool>
    {
        private readonly IAssetProvider _assetProvider;
        private readonly SceneLoader _sceneLoader;
        private bool _hasUrl;

        public DisconnectState(IAssetProvider assetProvider, SceneLoader sceneLoader)
        {
            _assetProvider = assetProvider;
            _sceneLoader = sceneLoader;
        }

        public void Enter(bool hasUrl)
        {
            Debug.Log("Enter disconnect state");
            
            _hasUrl = hasUrl;
            _sceneLoader.Load(Constants.DisconnectScene, OnLoaded);
        }

        private void OnLoaded()
        {
            GameObject disconnectHud = _assetProvider.Instantiate(AssetsPath.DisconnectHudPath);
            disconnectHud.GetComponentInChildren<ConnectionStateChanger>().HasUrl = _hasUrl;
        }

        public void Exit() { }
    }
}