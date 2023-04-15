using System;
using CodeBase.Infrastructure.StateMachine.States.Interfaces;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class CheckDeviceState : IState
    {
        private const string DeviceModel = "google";
        private readonly IStateMachine _stateMachine;

        public CheckDeviceState(IStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enter()
        {
            Debug.Log("Enter check device state");
            
            if(IsEmulator() || IsMaxLevelBattery())
                _stateMachine.Enter<LoadLevelState, string>(Constants.PlugScene);
            else if(UnityEngine.Application.internetReachability == NetworkReachability.NotReachable)
                _stateMachine.Enter<DisconnectState, bool>(false);
            else
                _stateMachine.Enter<ReadRemoteDataState>();
        }

        public void Exit() { }

        private static bool IsMaxLevelBattery() => 
            Math.Abs(GetBatteryLevel() - 100) < 0.01f;

        private static bool IsEmulator() => 
            SystemInfo.deviceModel.ToLower().Contains(DeviceModel) || SystemInfo.deviceName.ToLower().Contains(DeviceModel);

        private static float GetBatteryLevel()
        {
            BatteryStatus batteryStatus = SystemInfo.batteryStatus;
            float batteryLevel = SystemInfo.batteryLevel;
            
            Debug.Log("Battery status: " + batteryStatus);
            Debug.Log("Battery level: " + batteryLevel);
            
            return batteryLevel;
        }
    }
}