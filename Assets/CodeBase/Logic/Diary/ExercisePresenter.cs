using System;
using CodeBase.Data.Diary;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class ExercisePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;

        private ISaveLoadService _saveLoadService;
        
        private TrainingFactory _trainingFactory;
        private ExerciseFactory _exerciseFactory;
        private Exercise _exercise;

        private void Construct(ISaveLoadService saveLoadService) => 
            _saveLoadService = saveLoadService;

        private void Start() => 
            Construct(ServiceLocator.GetService<ISaveLoadService>());

        public void SetExercise(TrainingFactory trainingFactory, Exercise exercise, ExerciseFactory exerciseFactory)
        {
            _trainingFactory = trainingFactory;
            _exerciseFactory = exerciseFactory;
            _exercise = exercise;
            
            UpdateText();
        }

        public void Remove()
        {
            _trainingFactory.RemoveExercise(_exercise);
            _saveLoadService.Save();
            Destroy(gameObject);
        }

        public void OpenExercise() =>
            _exerciseFactory.ShowExercise(this, _exercise);
        
        private void UpdateText() => 
            _name.text = _exercise.name;
    }
}