using CodeBase.Data.Diary;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.TrainingDiary
{
    public class TrainingPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _date;

        private IPersistentSavedDataService _persistentSavedDataService;
        private Training _training;

        private Data.Diary.TrainingDiary Diary =>
            _persistentSavedDataService.SavedData.trainingDiary;

        private void Construct(IPersistentSavedDataService persistentSavedDataService) =>
            _persistentSavedDataService = persistentSavedDataService;

        private void Start() =>
            Construct(ServiceLocator.GetService<IPersistentSavedDataService>());

        public void Remove()
        {
            if (Diary.trainings.Contains(_training))
                Diary.trainings.Remove(_training);

            Destroy(gameObject);
        }

        public void SetTraining(Training training)
        {
            _training = training;
            UpdateText();
        }

        private void UpdateText()
        {
            _name.text = _training.name;
            _date.text = _training.date;
        }
    }
}