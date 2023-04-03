using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Timer
{
    public class LapsController : MonoBehaviour
    {
        [SerializeField] private Lap _lapPrefab;
        [SerializeField] private Transform _content;

        private readonly List<Lap> _laps = new();

        public void ClearLaps()
        {
            _laps.Clear();

            for (int i = 0; i < _content.childCount; i++)
            {
                Transform lap = _content.GetChild(i);
                Destroy(lap.gameObject);
            }
        }

        public void CreateLap(string time)
        {
            Lap lap = Instantiate(_lapPrefab, _content);
            _laps.Add(lap);
            
            InitializeLap(time, lap);
        }

        private void InitializeLap(string time, Lap lap)
        {
            lap.transform.SetAsFirstSibling();
            lap.SetText(GetNumberText(), time);
        }

        private string GetNumberText() =>
            $"Lap {_laps.Count}";
    }
}