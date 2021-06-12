namespace System
{
    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }
}