using System;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public event Action OnStop;

        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);

        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += onStop;
            }

            private void onStop()
            {
                _unitMovementStop.OnStop -= onStop;
                HandleValueChanged(new AsyncExtensions.Void());
            }

        }
    }
}