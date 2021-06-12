﻿using Abstractions;
using UI.Model;
using UnityEngine;
using Zenject;

namespace InputSystem.UI.Model
{
    public class ButtonPanel
    {
        [Inject] private CommandCreatorBase<IProduceUnitCommand> _produceUnitCommandCreator;
        [Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;
        [Inject] private CommandCreatorBase<IStopCommand> _stopCommandCreator;
        [Inject] private CommandCreatorBase<IAttackCommand> _attackCommandCreator;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patrolCommandCreator;

        private bool _isPending;

        public void HandleClick(ICommandExecutor commandExecutor)
        {
            CancelPendingCommand();
            
            _isPending = true;
            
            _produceUnitCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _moveCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _stopCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _attackCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _patrolCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
        }

        private void ExecuteSpecificCommand(ICommandExecutor commandExecutor, ICommand command)
        {
            commandExecutor.Execute(command);
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

            _produceUnitCommandCreator.CancelCommand();
            _moveCommandCreator.CancelCommand();
            _stopCommandCreator.CancelCommand();
            _attackCommandCreator.CancelCommand();
            _patrolCommandCreator.CancelCommand();
        }
    }
}