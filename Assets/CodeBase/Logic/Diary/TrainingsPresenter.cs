using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Diary;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class TrainingsPresenter : MonoBehaviour, ISaveProgressReader
    {
        [SerializeField] private TrainingPresenter _trainingPresenterPrefab;
        [SerializeField] private TrainingFactory _trainingFactory;
        [SerializeField] private Transform _trainingsContentRoot;

        private void OnEnable() => 
            _trainingFactory.OnTrainingCreated += TrainingCreatedHandler;

        private void OnDisable() => 
            _trainingFactory.OnTrainingCreated -= TrainingCreatedHandler;

        public void Load(SavedData savedData)
        {
            List<Training> trainings = savedData.trainingDiary.trainings;
            
            for (int i = trainings.Count - 1; i > 0; i--) 
                Create(trainings[i]);
        }

        private void TrainingCreatedHandler(Training training) => 
            Create(training);

        private void Create(Training training)
        {
            TrainingPresenter trainingUi = Instantiate(_trainingPresenterPrefab, _trainingsContentRoot);
            trainingUi.transform.SetSiblingIndex(0);

            trainingUi.SetTraining(training, _trainingFactory);
        }
    }
}