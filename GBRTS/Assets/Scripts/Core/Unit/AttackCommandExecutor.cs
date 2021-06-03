using Abstractions;
using UnityEngine;

namespace Core.Unit
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        protected override void ExecuteConcreteCommand(IAttackCommand command)
        {
            Debug.Log(name + "is attacking!");
        }
    }
}