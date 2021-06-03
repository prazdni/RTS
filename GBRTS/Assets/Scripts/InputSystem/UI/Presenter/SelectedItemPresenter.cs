using System;
using Abstractions;
using UI.Model;
using UI.View;
using UnityEngine;

namespace UI.Presenter
{
    public class SelectedItemPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedItem _item;
        [SerializeField] private SelectedItemView _view;
        
        protected void Start()
        {
            _item.OnValueChanged += UpdateView;
        }

        private void UpdateView(ISelectableItem selectableItem)
        {
            var hasItem = selectableItem != null;
            _view.gameObject.SetActive(hasItem);
            if (!hasItem)
                return;

            _view.Icon = selectableItem.Icon;
            _view.Text = $"{selectableItem.Health} / {selectableItem.MaxHealth}";
            _view.HealthPercent = selectableItem.Health / selectableItem.MaxHealth;
        }
    }
}