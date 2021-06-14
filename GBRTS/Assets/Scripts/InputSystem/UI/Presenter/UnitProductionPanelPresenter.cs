using System;
using Abstractions;
using InputSystem.UI.Model;
using UI.Model;
using UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace InputSystem.UI.Presenter
{
    public class UnitProductionPanelPresenter : MonoBehaviour
    {
        [SerializeField] private UnitProductionPanelView _view;
        [Inject] private SelectedItem _item;
        [Inject] private UnitProductionPanel _productionPanel;
        private ISelectableItem _currentSelectedItem;

        private void Start()
        {
            _item.Subscribe(HandleSelectionChanged);
        }

        private void UpdateView(CollectionAddEvent<IUnitProductionTask> addEvent)
        {
            _view.AddNewItem(addEvent);
        }

        private void HandleSelectionChanged(ISelectableItem item)
        {
            if (_currentSelectedItem == item)
            {
                return;
            }

            _currentSelectedItem = item;
            _productionPanel.HandleSelectionChanged(_currentSelectedItem);
            UpdateUnitProductionQueue();
        }

        private void UpdateUnitProductionQueue()
        {
            _view.ClearAll();
            
            if (_productionPanel.UnitProductionQueue == null)
            {
                return;
            }
            
            _view.DisplayQueue(_productionPanel.UnitProductionQueue);
            
            _productionPanel.UnitProductionQueue.ObserveAdd().Subscribe(UpdateView).AddTo(this);
        }
    }
}