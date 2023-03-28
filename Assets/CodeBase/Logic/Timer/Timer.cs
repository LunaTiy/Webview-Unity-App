using System;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private DateTime _startTime;
        private TimeSpan _elapsed;
        private bool _isStarted;

        private void Update()
        {
            _elapsed = DateTime.Now - _startTime;
            UpdateText();
        }

        public void StartTimer()
        {
            if (!_isStarted)
                _startTime = DateTime.Now;
            else
                _startTime = DateTime.Now - _elapsed;

            _isStarted = true;
            enabled = true;
        }

        public void StopTimer() => 
            enabled = false;

        private void UpdateText()
        {
            int milliseconds = _elapsed.Milliseconds < 100 ? _elapsed.Milliseconds : _elapsed.Milliseconds / 10;
            _text.text = $"{_elapsed.Minutes:D2}:{_elapsed.Seconds:D2}:{milliseconds:D2}";
        }
    }
}
