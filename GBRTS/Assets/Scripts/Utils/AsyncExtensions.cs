using System;
using System.Threading;
using System.Threading.Tasks;

public static class AsyncExtensions
{
    public struct Void { }
    public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
    {
        var voidTask = new TaskCompletionSource<Void>();
        using (cancellationToken.Register(token => ((TaskCompletionSource<Void>)token).TrySetResult(new Void()), voidTask))
        {
            var any = await Task.WhenAny(task, voidTask.Task);
            if (any == voidTask.Task)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
                
            return await task;
        }
    }
    public static Task<T> AsTask<T>(this IAwaitable<T> awaiter)
    {
        return Task.Run(async () => await awaiter);
    }
}