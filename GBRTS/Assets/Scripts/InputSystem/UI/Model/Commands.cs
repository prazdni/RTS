using Abstractions;
using UnityEngine;
using Zenject;

namespace System
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAsset("Ellen")] private GameObject _unit;
        public virtual GameObject UnitPrefab => _unit;
    }
    
    public class ProduceUnitCommandHeir : ProduceUnitCommand
    {
        [InjectAsset("Chomper")] private GameObject _unit;
        public override GameObject UnitPrefab => _unit;
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