using InputSystem.UI.Model;
using UniRx;

namespace System
{
    public abstract class StatelessScriptableObjectBase<T> : ScriptableObjectBase<T>, IObservable<T>
    {
        private Subject<T> _innerDataSource = new Subject<T>();

        public override void SetValue(T item)
        {
            base.SetValue(item);
            _innerDataSource.OnNext(CurrentValue);
        }

        public IDisposable Subscribe(IObserver<T> observer) => _innerDataSource.Subscribe(observer);
    }
}