using UnityEngine;

namespace Abstractions
{
    public interface ICommand
    {
        
    }
    
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
        IProductionTime ProductionTime { get; }
        IUnitSprite ProductionIcon { get; }
    }

    public interface IProduceUnitCommandEllen : IProduceUnitCommand
    {
        
    }
    
    public interface IProduceUnitCommandChomper : IProduceUnitCommand
    {
        
    }

    public interface IMoveCommand : ICommand
    {
        Vector3 Position { get; }
    }

    public interface IAttackCommand : ICommand
    {
        public IAttackable Target { get; }
    }

    public interface IStopCommand : ICommand
    {
        
    }

    public interface IPatrolCommand : ICommand
    {
        public Vector3 From { get; }
        public Vector3 To { get; }
    }

    public interface ISetRallyPointCommand : ICommand
    {
        public Vector3 RallyPoint { get; }
    }
}