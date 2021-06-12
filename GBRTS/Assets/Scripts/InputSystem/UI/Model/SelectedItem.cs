using System;
using Abstractions;
using InputSystem.UI.Model;
using UnityEngine;

namespace UI.Model
{
    [CreateAssetMenu(fileName = "SelectedItem", menuName = "Strategy/SelectedItem")]
    public class SelectedItem : StatefulScriptableObjectBase<ISelectableItem>
    {
        public Action<ISelectableItem> OnDeselected;
        
        public override void SetValue(ISelectableItem item)
        {
            OnDeselected?.Invoke(CurrentValue);
            
            base.SetValue(item);
        }
    }
}