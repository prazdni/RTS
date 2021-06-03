using Abstractions;
using UnityEngine;

namespace Core.Unit
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        protected override void ExecuteConcreteCommand(IMoveCommand command)
        {
            //TODO сделать плавное перемещение во времени
            transform.position = command.Position;
        }
    }
}