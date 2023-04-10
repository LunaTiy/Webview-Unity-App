using CodeBase.Data.Diary;
using CodeBase.Infrastructure.Container;
using CodeBase.Infrastructure.Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class SetPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _reps;
        [SerializeField] private TMP_Text _weight;
        
        private ISaveLoadService _saveLoadService;
        
        private ExerciseFactory _exerciseFactory;
        private SetFactory _setFactory;
        private Set _set;
        
        private void Construct(ISaveLoadService saveLoadService) => 
            _saveLoadService = saveLoadService;

        private void Start() => 
            Construct(ServiceLocator.GetService<ISaveLoadService>());

        public void SetTrainingSet(Set set, ExerciseFactory exerciseFactory = null, SetFactory setFactory = null)
        {
            if(exerciseFactory != null)
                _exerciseFactory = exerciseFactory;
            
            if(setFactory != null)
                _setFactory = setFactory;
            
            _set = set;
            UpdateText();
        }

        public void Remove()
        {
            _exerciseFactory.RemoveSet(_set);
            _saveLoadService.Save();
            Destroy(gameObject);
        }

        public void OpenSet() =>
            _setFactory.ShowSet(this, _set);

        private void UpdateText()
        {
            _reps.text = _set.reps.ToString();
            _weight.text = _set.weight.ToString();
        }
    }
}