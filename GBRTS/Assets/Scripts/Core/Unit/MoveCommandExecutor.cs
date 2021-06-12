using System;
using System.Threading;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;
        
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override async void ExecuteConcreteCommand(IMoveCommand command)
        {
            _navMeshAgent.SetDestination(command.Position);
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop.AsTask().WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.ResetPath();
            }
        }
    }
}