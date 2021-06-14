using Abstractions;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject] private ProduceUnitExecutorEllen _produceUnitEllenCommandExecutor;
        [Inject] private ProduceUnitExecutorChomper _produceUnitChomperCommandExecutor;
        [Inject] private CommandExecutorBase<ISetRallyPointCommand> _setRallyCommandExecutor;
        
        public async void EnqueueCommand(object command)
        {
            await _produceUnitEllenCommandExecutor.TryExecuteCommand(command);
            await _produceUnitChomperCommandExecutor.TryExecuteCommand(command);
            await _setRallyCommandExecutor.TryExecuteCommand(command);
        }

        public void Clear() { }
    }
}