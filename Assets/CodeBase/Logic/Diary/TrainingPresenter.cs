using CodeBase.Data.Diary;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class TrainingPresenter : MonoBehaviour
    {
        [SerializeField] private TrainingFactory _trainingFactory;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _date;

        private IPersistentSavedDataService _persistentSavedDataService;
        private ISaveLoadService _saveLoadService;

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

        public void OpenTraining() =>
            _trainingFactory.ShowTraining(_training, this);

        public void Remove()
        {
            if (Diary.trainings.Contains(_training))
                Diary.trainings.Remove(_training);

            _saveLoadService.Save();
            Destroy(gameObject);
        }

        public void SetTraining(Training training, TrainingFactory trainingFactory = null)
        {
            if (trainingFactory != null)
                _trainingFactory = trainingFactory;

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