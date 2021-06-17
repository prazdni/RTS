using System;
using System.Threading;
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
            if (commandExecutor is ICommandExecutor<T>)
            {
                CreateSpecificCommand(onCreate);
            }
        }

        protected abstract void CreateSpecificCommand(Action<T> onCreate);

        public virtual void CancelCommand() { }
    }
    
    public abstract class CancellableCommandCreatorBase<T, TParam> : CommandCreatorBase<T> where T : ICommand
    {
        [Inject] private IAwaitable<TParam> _param;
        private CancellationTokenSource _tokenSource;

        protected override async void CreateSpecificCommand(Action<T> onCreate)
        {
            _tokenSource = new CancellationTokenSource();
            try
            {
                var param = await _param.AsTask().WithCancellation(_tokenSource.Token);
                onCreate?.Invoke(_context.Inject(CreateSpecificCommand(param)));
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("Operation Canceled");
            }
        }

        protected abstract T CreateSpecificCommand(TParam param);

        public override void CancelCommand()
        {
            base.CancelCommand();

            if (_tokenSource == null)
            {
                return;
            }
            
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        }
    }
    
    public class ProduceUnitEllenCommandCreator : CommandCreatorBase<IProduceUnitCommandEllen>
    {
        protected override void CreateSpecificCommand(Action<IProduceUnitCommandEllen> onCreate)
        {
            onCreate?.Invoke(_context.Inject(new ProduceEllenUnitCommand()));
        }
    }
    
    public class ProduceUnitChomperCommandCreator : CommandCreatorBase<IProduceUnitCommandChomper>
    {
        protected override void CreateSpecificCommand(Action<IProduceUnitCommandChomper> onCreate)
        {
            onCreate?.Invoke(_context.Inject(new ProduceChomperUnitCommand()));
        }
    }
    
    public class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        protected override void CreateSpecificCommand(Action<IStopCommand> onStop)
        {
            onStop?.Invoke(_context.Inject(new StopCommand()));
        }
    }
    
    public class MoveCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {
        protected override IMoveCommand CreateSpecificCommand(Vector3 param) => new MoveCommand(param);
    }
    
    public class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateSpecificCommand(IAttackable param) => new AttackCommand(param);
    }
    
    public class PatrolCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        [Inject] private SelectedItem _selectedItem;
        protected override IPatrolCommand CreateSpecificCommand(Vector3 param) => new PatrolCommand(_selectedItem.CurrentValue.UnitTransform.position, param);
    }
    
    public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateSpecificCommand(Vector3 param) => new SetRallyPoint(param);
    }
}