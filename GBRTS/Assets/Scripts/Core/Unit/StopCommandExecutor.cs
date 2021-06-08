using System;
using System.Threading;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void ExecuteConcreteCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}