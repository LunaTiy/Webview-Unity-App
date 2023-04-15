using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Connecting
{
    public class ConnectionChecker : MonoBehaviour
    {
        public event Action InternetConnected;
        [SerializeField] private float _checkingInterval = 1f;
        
        private void Start() => 
            StartCoroutine(CheckConnectionRoutine());

        private IEnumerator CheckConnectionRoutine()
        {
            WaitForSeconds waitingTime = new WaitForSeconds(_checkingInterval);

            while (Application.internetReachability == NetworkReachability.NotReachable)
                yield return waitingTime;
            
            InternetConnected?.Invoke();
        }
    }
}
