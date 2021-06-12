using System;
using UnityEngine;

public abstract class AwaiterBase<TResult> : IAwaiter<TResult>
{
    private Action _continuation;
        
    public bool IsCompleted { get; private set; }
        
    private TResult _result;
        
    public TResult GetResult() => _result;
        
    public void OnCompleted(Action continuation)
    {
        Debug.Log("Completed");
        _continuation = continuation;
        if (IsCompleted)
        {
            continuation?.Invoke();
        }
    }

    protected void HandleValueChanged(TResult result)
    {
        IsCompleted = true;
        _result = result;
        _continuation?.Invoke();
    }
}