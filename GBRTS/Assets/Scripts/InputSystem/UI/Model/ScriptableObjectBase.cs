using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InputSystem.UI.Model
{
    public abstract class ScriptableObjectBase<T> : ScriptableObject, IAwaitable<T>
    {
        public Action<T> OnValueChanged;
        public T CurrentValue { get; protected set; }
        
        public virtual void SetValue(T item)
        {
            CurrentValue = item;
            OnValueChanged?.Invoke(item);
        }

        public IAwaiter<T> GetAwaiter() => new ValueChangedNotifier<T>(this);

        public class ValueChangedNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            private ScriptableObjectBase<TAwaited> _valueContainer;
            
            public ValueChangedNotifier(ScriptableObjectBase<TAwaited> valueContainer)
            {
                _valueContainer = valueContainer;
                valueContainer.OnValueChanged += HandleValueChanged;
            }

            protected override void HandleValueChanged(TAwaited result)
            {
                _valueContainer.OnValueChanged -= HandleValueChanged;
                base.HandleValueChanged(result);
            }
        }
    }
}