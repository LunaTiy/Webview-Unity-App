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

        public void Apply()
        {
            ConfigureSet();
            DisableScreen();
        }

        public void Cancel() => 
            DisableScreen();

        private void DisableScreen()
        {
            ResetInputFields();
            gameObject.SetActive(false);
        }

        private void ConfigureSet()
        {
            Set set = new();
            ParseValues(set);

            _exerciseFactory.AddSet(set);
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