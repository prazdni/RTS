using System;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public event Action<AsyncExtensions.Void> OnStop;
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private int _throttleFrames = 60;
        [SerializeField] private int _continuityThreshold = 10;
        
        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);

        private void Awake()
        {
            _collisionDetector.Collisions.Where(_ => _agent.hasPath)
                .Where(collision => collision.collider.GetComponentInParent<ISelectableItem>() != null)
                .Select(_ => Time.frameCount).Distinct().Buffer(_throttleFrames).Where(
                    buffer =>
                    {
                        for (int i = 1; i < buffer.Count; i++)
                        {
                            if (buffer[i] - buffer[i - 1] > _continuityThreshold)
                            {
                                return false;
                            }
                        }

                        return true;
                    })
                .Subscribe(_ =>
                {
                    _agent.isStopped = true;
                    _agent.ResetPath();
                    OnStop?.Invoke(new AsyncExtensions.Void());
                }).AddTo(this);
        }

        private void Update()
        {
            if (_agent.pathPending) return;
            if (!(_agent.remainingDistance <= _agent.stoppingDistance)) return;
            if ((_agent.hasPath && _agent.velocity.sqrMagnitude != 0f)) return;
            OnStop?.Invoke(new AsyncExtensions.Void());
        }

        private class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += HandleValueChanged;
            }

            protected override void HandleValueChanged(AsyncExtensions.Void result)
            {
                _unitMovementStop.OnStop -= HandleValueChanged;
                base.HandleValueChanged(result);
            }
        }
    }
}