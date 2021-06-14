using Abstractions;
using UI.Model;
using UnityEngine;
using Zenject;

namespace InputSystem.UI.Model
{
    public class ButtonPanel
    {
        [Inject] private CommandCreatorBase<IProduceUnitCommandEllen> _produceUnitEllenCommandCreator;
        [Inject] private CommandCreatorBase<IProduceUnitCommandChomper> _produceUnitChomperCommandCreator;
        [Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;
        [Inject] private CommandCreatorBase<IStopCommand> _stopCommandCreator;
        [Inject] private CommandCreatorBase<IAttackCommand> _attackCommandCreator;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patrolCommandCreator;
        [Inject] private CommandCreatorBase<ISetRallyPointCommand> _setRallyCommandCreator;

        private bool _isPending;

        public void HandleClick(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
        {
            CancelPendingCommand();
            
            _isPending = true;
            
            _produceUnitEllenCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandsQueue, command));
            _produceUnitChomperCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandsQueue, command));
            _moveCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandsQueue, command));
            _stopCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandsQueue, command));
            _attackCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandsQueue, command));
            _patrolCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandsQueue, command));
            _setRallyCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandsQueue, command));
        }

        private void ExecuteSpecificCommand(ICommandsQueue commandsQueue, ICommand command)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                commandsQueue.Clear();
            }

            commandsQueue.EnqueueCommand(command);
            
            _isPending = false;
        }

        public void HandleSelectionChanged()
        {
            CancelPendingCommand();
        }

        private void CancelPendingCommand()
        {
            if (!_isPending)
            {
                return;
            }

            _produceUnitEllenCommandCreator.CancelCommand();
            _produceUnitChomperCommandCreator.CancelCommand();
            _moveCommandCreator.CancelCommand();
            _stopCommandCreator.CancelCommand();
            _attackCommandCreator.CancelCommand();
            _patrolCommandCreator.CancelCommand();
        }
    }
}