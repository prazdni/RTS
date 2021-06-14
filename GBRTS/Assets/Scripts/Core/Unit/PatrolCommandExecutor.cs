using System;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        private NavMeshAgent _navMeshAgent;
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override Task ExecuteConcreteCommand(IPatrolCommand command)
        {
            return Task.Run(() =>
            {
                Patrol(command.From, command.To);
            });
        }

        private async void Patrol(Vector3 from, Vector3 to)
        {
            while (true)
            {
                await MoveTo(from);
                await MoveTo(to);
            }
        }

        private async Task MoveTo(Vector3 to)
        {
            _navMeshAgent.SetDestination(to);
            while ((transform.position - to).magnitude >= 1.0f)
            {
                await Task.Yield();
            }
        }
    }
}