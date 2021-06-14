using System;
using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using UI.Model;
using UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Presenter
{
    public class ButtonPanelPresenter : MonoBehaviour
    {
        [Inject] private SelectedItem _item;
        [SerializeField] private ButtonsPanelView _view;

        [Inject] private ButtonPanel _buttonPanel;
        private ISelectableItem _currentSelectedItem;

        protected void Start()
        {
            _item.Subscribe(HandleSelectionChanged);
            _view.OnClick += HandleClick;
            
            _view.ClearButtons();
        }

        private void HandleSelectionChanged(ISelectableItem item)
        {
            _buttonPanel.HandleSelectionChanged();
            SetButtons(item);
        }

        private void SetButtons(ISelectableItem item)
        {
            if (_currentSelectedItem == item)
            {
                return;
            }

            _currentSelectedItem = item;
            _view.ClearButtons();

            var commandExecutors = (_currentSelectedItem as Component)?.GetComponentsInParent<ICommandExecutor>().ToList();
            var queue = (_currentSelectedItem as Component)?.GetComponentInParent<ICommandsQueue>();
            Debug.Log(queue == null);
            _view.SetButtons(commandExecutors, queue);
        }

        private void HandleClick(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
        {
            _buttonPanel.HandleClick(commandExecutor, commandsQueue);
        }
    }
}