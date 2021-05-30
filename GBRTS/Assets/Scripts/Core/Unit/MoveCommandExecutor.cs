using Abstractions;
using UnityEngine;

namespace Core.Unit
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        protected override void ExecuteConcreteCommand(IMoveCommand command)
        {
            Debug.Log(name + " is moving!");
        }
    }
}