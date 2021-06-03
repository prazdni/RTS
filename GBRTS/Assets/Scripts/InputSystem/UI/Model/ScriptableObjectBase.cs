using System;
using Abstractions;
using UnityEngine;

namespace InputSystem.UI.Model
{
    public abstract class ScriptableObjectBase<T> : ScriptableObject
    {
        public Action<T> OnValueChanged;
        public T CurrentValue { get; protected set; }
        
        public virtual void SetValue(T item)
        {
            CurrentValue = item;
            OnValueChanged?.Invoke(item);
        }
    }
}