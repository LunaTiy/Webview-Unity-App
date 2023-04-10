using CodeBase.Data.Diary;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Diary
{
    public class SetFactory : MonoBehaviour, ITrainingFactory
    {
        [SerializeField] private ExerciseFactory _exerciseFactory;
        [SerializeField] private TMP_InputField _reps;
        [SerializeField] private TMP_InputField _weight;
        
        private SetPresenter _setPresenter;
        private bool _isCreated;
        private Set _set;

        public void Apply()
        {
            ConfigureSet();
            DisableScreen();
        }

        public void Cancel() => 
            DisableScreen();

        public void ShowSet(SetPresenter setPresenter, Set set)
        {
            _setPresenter = setPresenter;
            _isCreated = true;
            _set = set;
            
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);

            _reps.text = _set.reps.ToString();
            _weight.text = _set.weight.ToString();
        }

        private void DisableScreen()
        {
            ResetInputFields();
            _isCreated = false;
            _setPresenter = null;
            gameObject.SetActive(false);
        }

        private void ConfigureSet()
        {
            if (_isCreated)
            {
                ParseValues(_set);
                _setPresenter.SetTrainingSet(_set);
            }
            else
            {
                Set set = new();
                ParseValues(set);
                
                _exerciseFactory.AddSet(set);
            }
        }

        private void ParseValues(Set set)
        {
            if (int.TryParse(_reps.text, out int reps))
                set.reps = reps;
            
            if (int.TryParse(_weight.text, out int weight))
                set.weight = weight;
        }

        private void ResetInputFields()
        {
            _reps.text = string.Empty;
            _weight.text = string.Empty;
        }
    }
}