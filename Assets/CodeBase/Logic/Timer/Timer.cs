using System;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Timer
{
    public class Timer : MonoBehaviour
    {
        public event Action<TimerState> StateChanged;

        [SerializeField] private LapsController _lapsController;
        [SerializeField] private TMP_Text _text;

        private DateTime _startTime;
        private TimeSpan _elapsed;
        private bool _isStarted;

        private TimerState state;

        private void Update()
        {
            _elapsed = DateTime.Now - _startTime;

            if (_elapsed.Minutes >= 60) 
                StartLap();

            UpdateText();
        }

        public void StartTimer()
        {
            _startTime = !_isStarted ? DateTime.Now : DateTime.Now - _elapsed;

            _isStarted = true;
            enabled = true;

            ChangeState(TimerState.Start);
        }

        public void StopTimer()
        {
            ChangeState(TimerState.Stop);
            enabled = false;
        }

        public void Reset()
        {
            ResetTime();
            _lapsController.ClearLaps();
            
            _isStarted = false;
        }

        public void StartLap()
        {
            _lapsController.CreateLap(GetTimeText());
            ResetTime();
        }

        private void ResetTime()
        {
            _elapsed = new TimeSpan();
            _startTime = DateTime.Now;
            
            UpdateText();
        }

        private void ChangeState(TimerState newState)
        {
            state = newState;
            StateChanged?.Invoke(state);
        }

        private void UpdateText() => 
            _text.text = GetTimeText();

        private string GetTimeText()
        {
            int milliseconds = _elapsed.Milliseconds < 100 ? _elapsed.Milliseconds : _elapsed.Milliseconds / 10;
            return $"{_elapsed.Minutes:D2}:{_elapsed.Seconds:D2}:{milliseconds:D2}";
        }
    }
}