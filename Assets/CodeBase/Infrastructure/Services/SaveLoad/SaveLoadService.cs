using CodeBase.Data;
using CodeBase.Data.Extensions;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string SavedDataKey = "SavedData";
        
        private readonly IPersistentSavedDataService _persistentSavedDataService;

        public SaveLoadService(IPersistentSavedDataService persistentSavedDataService) => 
            _persistentSavedDataService = persistentSavedDataService;

        public void Save() => 
            PlayerPrefs.SetString(SavedDataKey, _persistentSavedDataService.SavedData.ToJson());

        public SavedData Load() => 
            PlayerPrefs.GetString(SavedDataKey)?.FromJson<SavedData>();
    }
}