using System.Runtime.CompilerServices;

namespace System
{
    public interface IAwaiter<TResult> : INotifyCompletion
    {
        bool IsCompleted { get; }
        TResult GetResult();
    }
}