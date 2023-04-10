using CodeBase.Data.Diary;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class TrainingsPresenter : MonoBehaviour
    {
        [SerializeField] private TrainingPresenter _trainingPresenterPrefab;
        [SerializeField] private TrainingFactory _trainingFactory;
        [SerializeField] private Transform _trainingsContentRoot;

        private void OnEnable() => 
            _trainingFactory.OnTrainingCreated += TrainingCreatedHandler;

        private void OnDisable() => 
            _trainingFactory.OnTrainingCreated -= TrainingCreatedHandler;

        private void TrainingCreatedHandler(Training training)
        {
            TrainingPresenter trainingUi = Instantiate(_trainingPresenterPrefab, _trainingsContentRoot);
            trainingUi.transform.SetSiblingIndex(0);

            trainingUi.SetTraining(training, _trainingFactory);
        }
    }
}