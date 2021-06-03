using Abstractions;
using UnityEngine;

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
        
    }

    public class StopCommand : IStopCommand
    {
        
    }
    
    public class MoveCommand : IMoveCommand
    {
        public Vector3 Position { get; }
    }

    public class PatrolCommand : IPatrolCommand
    {
        
    }
}