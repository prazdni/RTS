using System;
using System.Threading;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Unit
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource;

        protected override void ExecuteConcreteCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
            CancellationTokenSource = null;
        }
    }
}