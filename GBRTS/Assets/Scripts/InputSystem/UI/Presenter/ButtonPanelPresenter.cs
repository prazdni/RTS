using System;
using System.Linq;
using Abstractions;
using UI.Model;
using UI.View;
using UnityEngine;

namespace UI.Presenter
{
    public class ButtonPanelPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItem _item;
        [SerializeField] private ButtonsPanelView _view;

        [SerializeField] private AssetsContext _assetsContext;
        
        private ISelectableItem _currentSelectedItem;

        protected void Start()
        {
            _item.OnSelected += SetButtons;
            _view.OnClick += HandleClick;
            
            _view.ClearButtons();
        }

        private void SetButtons(ISelectableItem item)
        {
            if (_currentSelectedItem == item)
            {
                return;
            }

            _currentSelectedItem = item;
            _view.ClearButtons();

            var commandExecutors =
                (_currentSelectedItem as Component)?.GetComponentsInParent<ICommandExecutor>().ToList();
            _view.SetButtons(commandExecutors);
        }

        private void HandleClick(ICommandExecutor commandExecutor)
        {
            //Todo: разнести создание комманд

            if (commandExecutor is CommandExecutorBase<IProduceUnitCommand>)
            {
                commandExecutor.Execute(_assetsContext.Inject(new ProduceUnitCommandHeir()));
            }

            if (commandExecutor as CommandExecutorBase<IAttackCommand>)
            {
                commandExecutor.Execute(_assetsContext.Inject(new AttackCommand()));
            }
            
            if (commandExecutor as CommandExecutorBase<IStopCommand>)
            {
                commandExecutor.Execute(_assetsContext.Inject(new StopCommand()));
            }
            
            if (commandExecutor as CommandExecutorBase<IMoveCommand>)
            {
                commandExecutor.Execute(_assetsContext.Inject(new MoveCommand()));
            }
            
            if (commandExecutor as CommandExecutorBase<IPatrolCommand>)
            {
                commandExecutor.Execute(_assetsContext.Inject(new PatrolCommand()));
            }
        }
    }
}