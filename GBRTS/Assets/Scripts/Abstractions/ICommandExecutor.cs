using System;
using UnityEngine;

namespace Abstractions
{
    public interface ICommandExecutor
    {
        void Execute(ICommand command);
    }

    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        public void Execute(ICommand command)
        {
            ExecuteConcreteCommand((T)command);
        }

        protected abstract void ExecuteConcreteCommand(T command);
    }
}