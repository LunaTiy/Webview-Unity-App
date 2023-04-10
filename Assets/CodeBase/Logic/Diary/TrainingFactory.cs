using System;
using System.Collections.Generic;
using CodeBase.Data.Diary;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class TrainingFactory : MonoBehaviour, ITrainingFactory
    {
        public event Action<Training> OnTrainingCreated;

        [SerializeField] private ExercisePresenter _exercisePresenterPrefab;
        [SerializeField] private Transform _exercisesRoot;

        [SerializeField] private TMP_InputField _date;
        [SerializeField] private TMP_InputField _name;

        private IPersistentSavedDataService _persistentSavedDataService;
        private ISaveLoadService _saveLoadService;
        private List<Exercise> _exercises = new();

        private TrainingDiary Diary =>
            _persistentSavedDataService.SavedData.trainingDiary;

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
            _saveLoadService.Save();
            
            ResetInputArea();
            ClearExercises();

            gameObject.SetActive(false);
        }

        public void Cancel()
        {
            ResetInputArea();
            ClearExercises();
            gameObject.SetActive(false);
        }

        public void AddExercise(Exercise exercise)
        {
            _exercises.Add(exercise);
            
            ExercisePresenter exercisePresenter = Instantiate(_exercisePresenterPrefab, _exercisesRoot);
            exercisePresenter.SetExercise(this, exercise);
            
            exercisePresenter.transform.SetSiblingIndex(0);
        }

        public void RemoveExercise(Exercise exercise)
        {
            if (_exercises.Contains(exercise))
                _exercises.Remove(exercise);
        }

        private void ConfigureData()
        {
            Training training = new()
            {
                date = _date.text,
                name = _name.text,
                exercises = _exercises
            };

            Diary.trainings.Add(training);
            ClearExercises();

            OnTrainingCreated?.Invoke(training);
        }

        private void ClearExercises()
        {
            int childCount = _exercisesRoot.childCount;

            for (int i = childCount - 2; i != -1; i--) 
                Destroy(_exercisesRoot.GetChild(i).gameObject);

            _exercises = new List<Exercise>();
        }

        private void ResetInputArea()
        {
            _date.text = string.Empty;
            _name.text = string.Empty;
        }
    }
}