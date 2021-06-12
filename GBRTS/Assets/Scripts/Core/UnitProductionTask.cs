using Abstractions;
using UnityEngine;

namespace Core
{
    public class UnitProductionTask : IUnitProductionTask
    {
        public Sprite Icon { get; }
        public float ProductionTimeLeft { get; set; }
        public float ProductionTime { get; }
        public GameObject UnitPrefab { get; }

        public UnitProductionTask(Sprite icon, int productionTime, GameObject unitPrefab)
        {
            Icon = icon;
            ProductionTimeLeft = productionTime;
            ProductionTime = productionTime;
            UnitPrefab = unitPrefab;
        }
    }
}