using System;
using UnityEngine;

namespace Abstractions
{
    public interface ICommandExecutor
    {
        void Execute(ICommand command);
    }
    
    public interface ICommandExecutor<T> : ICommandExecutor where T : ICommand
    {
    }
}