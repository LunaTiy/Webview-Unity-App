using System;
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
        public event Action<Training> OnTrainingCreated;

        [SerializeField] private TMP_InputField _date;
        [SerializeField] private TMP_InputField _name;

        private IPersistentSavedDataService _persistentSavedDataService;
        private ISaveLoadService _saveLoadService;
        private readonly List<Exercise> _exercises = new();

        private Data.Diary.TrainingDiary Diary => _persistentSavedDataService.SavedData.trainingDiary;

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

            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            ResetInputArea();
            gameObject.SetActive(false);
        }

        private void ConfigureData()
        {
            Training training = new()
            {
                date = _date.text,
                name =  _name.text,
                exercises = _exercises
            };

            Diary.trainings.Add(training);
            _exercises.Clear();

            OnTrainingCreated?.Invoke(training);
        }

        private void ResetInputArea()
        {
            _date.text = string.Empty;
            _name.text = string.Empty;
        }
    }
}