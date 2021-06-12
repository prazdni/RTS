using System;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public event Action OnStop;

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);

        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += OnStop;
            }

            private void OnStop()
            {
                _unitMovementStop.OnStop -= OnStop;
                HandleValueChanged(new AsyncExtensions.Void());
            }
        }
    }
}