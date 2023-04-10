using CodeBase.Data.Diary;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class ExercisePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        
        private TrainingFactory _trainingFactory;
        private ExerciseFactory _exerciseFactory;
        private Exercise _exercise;

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
            Destroy(gameObject);
        }

        public void OpenExercise() =>
            _exerciseFactory.ShowExercise(this, _exercise);
        
        private void UpdateText() => 
            _name.text = _exercise.name;
    }
}