using UniRx;

namespace Abstractions
{
    public interface IUnitProducer
    {
        IReactiveCollection<IUnitProductionTask> Queue { get; }
    }
}