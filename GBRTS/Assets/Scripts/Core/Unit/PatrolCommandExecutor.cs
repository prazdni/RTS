using Abstractions;
using UnityEngine;

namespace Core.Unit
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        protected override void ExecuteConcreteCommand(IPatrolCommand command)
        {
            Debug.Log($"{name} is patrolling!");
        }
    }
}