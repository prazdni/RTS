using System;
using Abstractions;
using InputSystem.UI.Model;
using UnityEngine;
using Zenject;

namespace UI.Model
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        [Inject] protected AssetsContext _context;
        
        public void CreateCommand(ICommandExecutor commandExecutor, Action<T> onCreate)
        {
            if (commandExecutor as CommandExecutorBase<T>)
            {
                CreateSpecificCommand(onCreate);
            }
        }

        protected abstract void CreateSpecificCommand(Action<T> onCreate);
        
        public virtual void ProcessCancel() { }
    }
    
    public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        protected override void CreateSpecificCommand(Action<IProduceUnitCommand> onCreate)
        {
            onCreate?.Invoke(_context.Inject(new ProduceUnitCommand()));
        }
    }
    
    public class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        protected override void CreateSpecificCommand(Action<IStopCommand> onStop)
        {
            onStop?.Invoke(_context.Inject(new StopCommand()));
        }
    }
    
    public class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _context;
        
        private Action<IMoveCommand> _onMove;

        [Inject]
        private void Init(Vector3Value currentGroundPosition)
        {
            currentGroundPosition.OnValueChanged += HandleCurrentGroundPositionChanged;
        }

        private void HandleCurrentGroundPositionChanged(Vector3 currentGroundPosition)
        {
            _onMove?.Invoke(_context.Inject(new MoveCommand(currentGroundPosition)));
        }
        
        protected override void CreateSpecificCommand(Action<IMoveCommand> onCreate)
        {
            _onMove = onCreate;
        }
        
        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _onMove = null;
        }
    }
    
    public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;
        
        private Action<IAttackCommand> _onAttack;

        [Inject]
        private void Init(AttackableValue currentAttackableValue)
        {
            currentAttackableValue.OnValueChanged += HandleCurrentGroundPositionChanged;
        }

        private void HandleCurrentGroundPositionChanged(IAttackable currentAttackable)
        {
            _onAttack?.Invoke(_context.Inject(new AttackCommand(currentAttackable)));
            _onAttack = null;
        }

        protected override void CreateSpecificCommand(Action<IAttackCommand> onCreate)
        {
            _onAttack = onCreate;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _onAttack = null;
        }
    }
    
    public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private SelectedItem _selectedItem;
        
        private Action<IPatrolCommand> _onPatrol;

        [Inject]
        private void Init(Vector3Value currentGroundPosition)
        {
            currentGroundPosition.OnValueChanged += HandleCurrentGroundPositionChanged;
        }

        private void HandleCurrentGroundPositionChanged(Vector3 groundClick)
        {
            _onPatrol?.Invoke(_context.Inject(new PatrolCommand(_selectedItem.CurrentValue.UnitTransform.position, groundClick)));
            _onPatrol = null;
        }

        protected override void CreateSpecificCommand(Action<IPatrolCommand> onPatrol)
        {
            _onPatrol = onPatrol;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _onPatrol = null;
        }
    }
}