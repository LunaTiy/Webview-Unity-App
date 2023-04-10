using CodeBase.Data.Diary;
using UnityEngine;

namespace CodeBase.Logic.TrainingDiary
{
    public class TrainingsPresenter : MonoBehaviour
    {
        [SerializeField] private TrainingPresenter _trainingPresenterPrefab;
        [SerializeField] private TrainingCreator _creator;
        [SerializeField] private Transform _trainingsContentRoot;

        private void OnEnable() => 
            _creator.OnTrainingCreated += TrainingCreatedHandler;

        private void OnDisable() => 
            _creator.OnTrainingCreated -= TrainingCreatedHandler;

        private void TrainingCreatedHandler(Training training)
        {
            TrainingPresenter trainingUi = Instantiate(_trainingPresenterPrefab, _trainingsContentRoot);
            trainingUi.transform.SetSiblingIndex(0);

            trainingUi.SetTraining(training);
        }
    }
}