using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Core.Unit
{
    public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        [Inject(Id = "Distance")] private float _attackDistance = 1.0f;
        [Inject(Id = "AttackSpeed")] private float _attackSpeed = 0.3f;
        [Inject(Id = "AttackDamage")] private float _attackDamage = 100.0f;

        private AttackOperation _currentAttackOperation;
        private IAttackable _target;
        private Vector3 _targetPosition;
        private Vector3 _attackerPosition;

        private Subject<Vector3> _destination = new Subject<Vector3>();
        private Subject<IAttackable> _attackTarget = new Subject<IAttackable>();

        [Inject]
        private void Init()
        {
            _destination.ObserveOn(Scheduler.MainThread).Subscribe(MoveTo).AddTo(this);
            _attackTarget.ObserveOn(Scheduler.MainThread).Subscribe(AttackTarget).AddTo(this);
        }

        public override Task ExecuteConcreteCommand(IAttackCommand command)
        {
            return Task.Run(async () =>
            {
                _target = command.Target;
                _currentAttackOperation = new AttackOperation(_target, this);
                try
                {
                    Debug.Log(name + "is attacking!");
                    await _currentAttackOperation;
                    _currentAttackOperation = null;
                }
                catch(Exception e)
                {
                    Debug.Log("AttackCancelled");
                }
            });
        }

        protected void Update()
        {
            if (!gameObject.activeSelf || _target == null || _currentAttackOperation == null)
            {
                return;
            }
            
            lock (this)
            {
                _attackerPosition = gameObject.transform.position;
                _targetPosition = _target.Position;
            }
        }

        private void MoveTo(Vector3 to)
        {
            if (gameObject.activeSelf && _navMeshAgent.isActiveAndEnabled)
            {
                _navMeshAgent.destination = to;
            }
        }

        private void AttackTarget(IAttackable target)
        {
            if (_navMeshAgent.isActiveAndEnabled)
            {
                _navMeshAgent.ResetPath();
            }

            target.ReceiveDamage(_attackDamage);

            if (target.Health <= 0 && _currentAttackOperation != null)
            {
                _currentAttackOperation.Cancel();
                _currentAttackOperation = null;
            }
        }
    }
}