using System;
using System.Collections.Generic;
using CodeBase.Data;
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

        [SerializeField] private ExerciseFactory _exerciseFactory;
        [SerializeField] private ExercisePresenter _exercisePresenterPrefab;
        [SerializeField] private Transform _exercisesRoot;

        [SerializeField] private TMP_InputField _date;
        [SerializeField] private TMP_InputField _name;

        private IPersistentSavedDataService _persistentSavedDataService;
        private ISaveLoadService _saveLoadService;
        private List<Exercise> _exercises = new();

        private TrainingPresenter _trainingPresenter;
        private bool _isCreated;
        private Training _training;

        private TrainingDiary Diary =>
            _persistentSavedDataService.SavedData.trainingDiary;

        private void Construct(IPersistentSavedDataService persistentSavedDataService, ISaveLoadService saveLoadService)
        {
            _persistentSavedDataService = persistentSavedDataService;
            _saveLoadService = saveLoadService;
        }

        private void Start() =>
            Construct(ServiceLocator.GetService<IPersistentSavedDataService>(),
                ServiceLocator.GetService<ISaveLoadService>());

        public void Apply()
        {
            ConfigureData();
            _saveLoadService.Save();

            ResetInputArea();
            ClearExercises();

            Disable();
        }

        public void Cancel()
        {
            ResetInputArea();
            ClearExercises();
            
            _saveLoadService.Save();

            Disable();
        }

        public void ShowTraining(Training training, TrainingPresenter trainingPresenter)
        {
            _trainingPresenter = trainingPresenter;
            _training = training;
            _isCreated = true;
            
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
            
            _exercises.Clear();

            for (int i = training.exercises.Count - 1; i >= 0; i--)
            {
                Exercise exercise = training.exercises[i];
                AddExercise(exercise);
            }

            _date.text = training.date;
            _name.text = training.name;
        }

        public void AddExercise(Exercise exercise)
        {
            _exercises.Add(exercise);

            ExercisePresenter exercisePresenter = Instantiate(_exercisePresenterPrefab, _exercisesRoot);
            exercisePresenter.SetExercise(this, exercise, _exerciseFactory);

            exercisePresenter.transform.SetSiblingIndex(0);
        }

        public void RemoveExercise(Exercise exercise)
        {
            if (_exercises.Contains(exercise))
                _exercises.Remove(exercise);
        }

        private void Disable()
        {
            _isCreated = false;
            _trainingPresenter = null;
            gameObject.SetActive(false);
        }

        private void ConfigureData()
        {
            if (_isCreated)
            {
                _training.date = _date.text;
                _training.name = _name.text;
                _training.exercises = _exercises;
                
                _trainingPresenter.SetTraining(_training);
            }
            else
            {
                Training training = new()
                {
                    date = _date.text,
                    name = _name.text,
                    exercises = _exercises
                };

                Diary.trainings.Add(training);
                OnTrainingCreated?.Invoke(training);
            }
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