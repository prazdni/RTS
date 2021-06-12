using UnityEngine;

namespace Abstractions
{
    public interface IUnitProductionTask
    {
        Sprite Icon { get; }
        float ProductionTimeLeft { get; set; }
        float ProductionTime { get; }
        public GameObject UnitPrefab { get; }
    }
}