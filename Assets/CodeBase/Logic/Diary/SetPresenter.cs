using CodeBase.Data.Diary;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class SetPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _reps;
        [SerializeField] private TMP_Text _weight;
        
        private ExerciseFactory _exerciseFactory;
        private Set _set;

        public void SetTrainingSet(ExerciseFactory exerciseFactory, Set set)
        {
            _exerciseFactory = exerciseFactory;
            _set = set;
            
            UpdateText();
        }

        public void Remove()
        {
            _exerciseFactory.RemoveSet(_set);
            Destroy(gameObject);
        }

        private void UpdateText()
        {
            _reps.text = _set.reps.ToString();
            _weight.text = _set.weight.ToString();
        }
    }
}