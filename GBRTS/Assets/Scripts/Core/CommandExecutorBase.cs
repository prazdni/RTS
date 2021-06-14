using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace Core
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<T> where T : class, ICommand
    {
        public void Execute(ICommand command) => ExecuteConcreteCommand((T)command);

        public async Task TryExecuteCommand(object command)
        {
            var concreteCommand = (T)command;
            if (concreteCommand != null)
            {
                await ExecuteConcreteCommand(concreteCommand);
            }
        }

        public abstract Task ExecuteConcreteCommand(T command);
    }
}