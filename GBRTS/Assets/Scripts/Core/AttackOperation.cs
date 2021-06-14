using System;
using System.Threading;
using Abstractions;
using Core.Unit;
using UnityEngine;
using Zenject;

namespace Core.Unit
{
    public partial class AttackCommandExecutor
    {
        private class AttackOperation : IAwaitable<AsyncExtensions.Void>
        {
            public event Action<AsyncExtensions.Void> OnComplete;
            
            private readonly AttackCommandExecutor _attacker;
            private readonly IAttackable _target;
            private bool _isCancelled;
            
            public AttackOperation(IAttackable target, AttackCommandExecutor attacker)
            {
                _target = target;
                _attacker = attacker;
                Thread thread = new Thread(AttackRoutine);
                thread.Start();
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new AttackOperationAwaiter(this);
            
            private void AttackRoutine(object obj)
            {
                while (true)
                {
                    if (_isCancelled || _target.Health <= 0)
                    {
                        OnComplete?.Invoke(new AsyncExtensions.Void());
                        return;
                    }

                    Vector3 targetPosition;
                    Vector3 attackerPosition;
                    
                    lock (_attacker)
                    {
                        targetPosition = _attacker._targetPosition;
                        attackerPosition = _attacker._attackerPosition;
                    }

                    var distance = (targetPosition - attackerPosition).magnitude;
                    if (distance > _attacker._attackDistance)
                    {
                        _attacker._destination.OnNext(targetPosition);
                        Thread.Sleep(200);
                    }
                    else
                    {
                        _attacker._attackTarget.OnNext(_target);
                        Thread.Sleep((int)(_attacker._attackSpeed * 1000));
                    }
                }
            }

            public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
            {
                private AttackOperation _attackOperation;

                public AttackOperationAwaiter(AttackOperation attackOperation)
                {
                    _attackOperation = attackOperation;
                    _attackOperation.OnComplete += HandleValueChanged;
                }

                protected override void HandleValueChanged(AsyncExtensions.Void result)
                {
                    _attackOperation.OnComplete -= HandleValueChanged;
                    base.HandleValueChanged(result);
                }
            }

            public void Cancel()
            {
                _isCancelled = true;
            }
        }
    }
}