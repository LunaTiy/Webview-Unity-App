using UnityEngine;

namespace CodeBase.Logic.Timer
{
    public class ButtonStateSwitcher : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        
        [SerializeField] private GameObject _buttonStart;
        [SerializeField] private GameObject _buttonStop;
        [SerializeField] private GameObject _buttonLap;
        [SerializeField] private GameObject _buttonReset;

        private void OnEnable() => 
            _timer.StateChanged += TimerStateChangedHandler;

        private void OnDisable() => 
            _timer.StateChanged -= TimerStateChangedHandler;

        private void TimerStateChangedHandler(TimerState state)
            => SelectAction(state);

        private void SelectAction(TimerState state) => 
            ChangeButtonsState(state == TimerState.Start);

        private void ChangeButtonsState(bool isStart)
        {
            _buttonStart.SetActive(!isStart);
            _buttonStop.SetActive(isStart);
            _buttonLap.SetActive(isStart);
            _buttonReset.SetActive(!isStart);
        }
    }
}