using System.Collections.Generic;
using CodeBase.Data.Diary;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class ExerciseFactory : MonoBehaviour, ITrainingFactory
    {
        [SerializeField] private SetPresenter _setPresenterPrefab;
        [SerializeField] private Transform _setsRoot;

        [SerializeField] private TrainingFactory _trainingFactory;
        [SerializeField] private TMP_InputField _name;

        private List<Set> _sets = new();

        public void Apply()
        {
            ConfigureExercise();
            DisableScreen();
            ClearSets();
        }

        public void Cancel()
        {
            DisableScreen();
            ClearSets();
        }

        public void AddSet(Set set)
        {
            _sets.Add(set);

            SetPresenter setPresenter = Instantiate(_setPresenterPrefab, _setsRoot);
            setPresenter.SetTrainingSet(this, set);

            setPresenter.transform.SetSiblingIndex(0);
        }

        public void RemoveSet(Set set)
        {
            if (_sets.Contains(set))
                _sets.Remove(set);
        }

        private void DisableScreen()
        {
            ResetInputFields();
            gameObject.SetActive(false);
        }

        private void ConfigureExercise()
        {
            Exercise exercise = new()
            {
                name = _name.text,
                sets = _sets
            };

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