using System.Collections.Generic;
using CodeBase.Data.Diary;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class ExerciseFactory : MonoBehaviour, ITrainingFactory
    {
        [SerializeField] private SetFactory _setFactory;
        [SerializeField] private SetPresenter _setPresenterPrefab;
        [SerializeField] private Transform _setsRoot;

        [SerializeField] private TrainingFactory _trainingFactory;
        [SerializeField] private TMP_InputField _name;

        private List<Set> _sets = new();

        private ExercisePresenter _exercisePresenter;
        private bool _isCreated;

        public void Apply()
        {
            ConfigureExercise();
            ClearSets();
            Disable();
        }

        public void Cancel()
        {
            ClearSets();
            Disable();
        }

        public void AddSet(Set set)
        {
            _sets.Add(set);

            SetPresenter setPresenter = Instantiate(_setPresenterPrefab, _setsRoot);
            setPresenter.SetTrainingSet(set, this, _setFactory);

            setPresenter.transform.SetSiblingIndex(0);
        }

        public void RemoveSet(Set set)
        {
            if (_sets.Contains(set))
                _sets.Remove(set);
        }

        public void ShowExercise(ExercisePresenter exercisePresenter, Exercise exercise)
        {
            _exercisePresenter = exercisePresenter;
            _isCreated = true;
            
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
            
            _sets.Clear();

            for (int i = exercise.sets.Count - 1; i >= 0; i--)
            {
                Set set = exercise.sets[i];
                AddSet(set);
            }
        }

        private void Disable()
        {
            ResetInputFields();
            _exercisePresenter = null;
            _isCreated = false;
            gameObject.SetActive(false);
        }

        private void ConfigureExercise()
        {
            Exercise exercise = new()
            {
                name = _name.text,
                sets = _sets
            };

            if (_isCreated)
                _exercisePresenter.SetExercise(_trainingFactory, exercise, this);
            else
                _trainingFactory.AddExercise(exercise);
        }

        private void ClearSets()
        {
            int childCount = _setsRoot.childCount;

            for (int i = childCount - 2; i != -1; i--) 
                Destroy(_setsRoot.GetChild(i).gameObject);

            _sets = new List<Set>();
        }

        private void ResetInputFields() =>
            _name.text = string.Empty;
    }
}