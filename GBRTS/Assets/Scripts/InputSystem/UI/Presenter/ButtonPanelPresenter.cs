using System;
using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using UI.Model;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.Presenter
{
    public class ButtonPanelPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItem _item;
        [SerializeField] private ButtonsPanelView _view;

        [Inject] private ButtonPanel _buttonPanel;
        private ISelectableItem _currentSelectedItem;

        protected void Start()
        {
            _item.OnValueChanged += SetButtons;
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
            _buttonPanel.HandleClick(commandExecutor);
        }
    }
}