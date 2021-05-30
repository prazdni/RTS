using Abstractions;
using UnityEngine;

namespace Core.Unit
{
    public class StopCommand : CommandExecutorBase<IStopCommand>
    {
        protected override void ExecuteConcreteCommand(IStopCommand command)
        {
            Debug.Log(name + " has stopped!");
        }
    }
}