using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class PlugFactory : IPlugFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly List<ISaveProgressReader> _savedProgressReaders = new();

        public IEnumerable<ISaveProgressReader> SaveProgressReaders => _savedProgressReaders;

        public PlugFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;
        
        public GameObject CreatePlug()
        {
            GameObject gameObject = _assetProvider.Instantiate(AssetsPath.PlugPath);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISaveProgressReader progressReader in gameObject.GetComponentsInChildren<ISaveProgressReader>())
                _savedProgressReaders.Add(progressReader);
        }
    }
}