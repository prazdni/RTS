using Abstractions;
using UnityEngine;

namespace Core
{
    public class ProduceUnitExecutor : CommandExecutorBase<IProduceUnitCommand>
    {
        protected override void ExecuteConcreteCommand(IProduceUnitCommand command)
        {
            if (command.UnitPrefab == null)
            {
                Debug.Log("No prefab to produce unit");
                return;
            }

            var transform1 = transform;
            Instantiate(command.UnitPrefab, transform1.position + Vector3.forward * 3, Quaternion.identity, transform1.parent);
        }
    }
}