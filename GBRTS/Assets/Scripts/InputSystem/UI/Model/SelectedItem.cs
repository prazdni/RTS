using System;
using Abstractions;
using InputSystem.UI.Model;
using UnityEngine;

namespace UI.Model
{
    [CreateAssetMenu(fileName = "SelectedItem", menuName = "Strategy/SelectedItem")]
    public class SelectedItem : ScriptableObjectBase<ISelectableItem>
    {
        public Action<ISelectableItem> OnDeselected;
        
        public override void SetValue(ISelectableItem item)
        {
            OnDeselected?.Invoke(item);
            
            base.SetValue(item);
        }
    }
}