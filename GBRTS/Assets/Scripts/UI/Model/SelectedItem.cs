using System;
using Abstractions;
using UnityEngine;

namespace UI.Model
{
    [CreateAssetMenu(fileName = "SelectedItem", menuName = "Strategy/SelectedItem")]
    public class SelectedItem : ScriptableObject
    {
        public Action<ISelectableItem> OnSelected;
        public Action<ISelectableItem> OnDeselected;
        private ISelectableItem _currentValue;

        public void SetValue(ISelectableItem item)
        {
            OnDeselected?.Invoke(_currentValue);
            _currentValue = item;
            OnSelected?.Invoke(item);
        }
    }
}