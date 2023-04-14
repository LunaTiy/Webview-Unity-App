using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class CheckDeviceTypeState : IState
    {
        private const string DeviceModel = "google";
        private readonly IStateMachine _stateMachine;

        public CheckDeviceTypeState(IStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enter()
        {
            Debug.Log("Enter check device state");
            
            if(IsEmulator())
                _stateMachine.Enter<LoadLevelState, string>(Constants.PlugScene);
            else
                _stateMachine.Enter<LoadSavedDataState>();
        }

        public void Exit() { }

        private static bool IsEmulator() => 
            SystemInfo.deviceModel.ToLower().Contains(DeviceModel) || SystemInfo.deviceName.ToLower().Contains(DeviceModel);
    }
}