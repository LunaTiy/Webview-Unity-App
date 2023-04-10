using System.Collections.Generic;
using CodeBase.Data.Diary;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.TrainingDiary
{
    public class TrainingCreator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _date;
        [SerializeField] private TMP_Text _weight;

        private IPersistentSavedDataService _persistentSavedDataService;
        private ISaveLoadService _saveLoadService;
        private readonly List<Exercise> _exercises = new();

        private void Construct(IPersistentSavedDataService persistentSavedDataService, ISaveLoadService saveLoadService)
        {
            _persistentSavedDataService = persistentSavedDataService;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            Construct(ServiceLocator.GetService<IPersistentSavedDataService>(),
                ServiceLocator.GetService<ISaveLoadService>());
        }

        public void Apply()
        {
            ConfigureData();
            ResetInputArea();
            
            _exercises.Clear();
            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            ResetInputArea();
            gameObject.SetActive(false);
        }

        private void ConfigureData()
        {
            Training training = new() { date = _date.text };

            if (float.TryParse(_weight.text, out float weight))
                training.humanWeight = weight;

            training.exercises = _exercises;
        }

        private void ResetInputArea()
        {
            _date.text = string.Empty;
            _weight.text = string.Empty;
        }
    }
}