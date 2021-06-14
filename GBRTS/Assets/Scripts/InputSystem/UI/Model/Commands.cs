using Abstractions;
using InputSystem.UI.Model;
using UnityEngine;
using Zenject;

namespace System
{
    public class ProduceEllenUnitCommand : IProduceUnitCommandEllen
    {
        [InjectAsset("Ellen")] private GameObject _unit;
        public GameObject UnitPrefab => _unit;
        public IProductionTime ProductionTime => _productionTime;
        [Inject(Id = "Ellen")] private EllenProductionTime _productionTime;
        public IUnitSprite ProductionIcon => _productionIcon;
        [Inject(Id = "Ellen")] private EllenUnitSprite _productionIcon;
    }
    
    public class ProduceChomperUnitCommand : IProduceUnitCommandChomper
    {
        [InjectAsset("Chomper")] private GameObject _unit;
        public GameObject UnitPrefab => _unit;
        public IProductionTime ProductionTime => _productionTime;
        [Inject(Id = "Chomper")] private ChomperProductionTime _productionTime;
        public IUnitSprite ProductionIcon => _productionIcon;
        [Inject(Id = "Chomper")] private ChomperUnitSprite _productionIcon;
    }

    public class AttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AttackCommand(IAttackable target)
        {
            Target = target;
        }
    }

    public class StopCommand : IStopCommand
    {
        
    }
    
    public class MoveCommand : IMoveCommand
    {
        public Vector3 Position { get; }

        public MoveCommand(Vector3 position)
        {
            Position = position;
        }
    }

    public class PatrolCommand : IPatrolCommand
    {
        public Vector3 From { get; }
        public Vector3 To { get; }

        public PatrolCommand(Vector3 from, Vector3 to)
        {
            From = from;
            To = to;
        }
    }
}