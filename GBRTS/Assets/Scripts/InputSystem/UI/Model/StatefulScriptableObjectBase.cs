using InputSystem.UI.Model;
using UniRx;

namespace System
{
    public abstract class StatefulScriptableObjectBase<T> : ScriptableObjectBase<T>, IObservable<T>
    {
        private ReactiveProperty<T> _innerDataSource = new ReactiveProperty<T>();

        public override void SetValue(T item)
        {
            base.SetValue(item);
            _innerDataSource.Value = CurrentValue;
        }

        public IDisposable Subscribe(IObserver<T> observer) => _innerDataSource.Subscribe(observer);
    }
}