using CodeBase.Data.Diary;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class ExercisePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        
        private TrainingFactory _trainingFactory;
        private Exercise _exercise;

        public void SetExercise(TrainingFactory trainingFactory, Exercise exercise)
        {
            _trainingFactory = trainingFactory;
            _exercise = exercise;
            
            UpdateText();
        }

        public void Remove()
        {
            _trainingFactory.RemoveExercise(_exercise);
            Destroy(gameObject);
        }

        private void UpdateText() => 
            _name.text = _exercise.name;
    }
}