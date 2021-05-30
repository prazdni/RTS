using System;
using Abstractions;
using QuickOutline;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(SelectableItemCharacteristics))]
    public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>
    {
        protected override void ExecuteConcreteCommand(IProduceUnitCommand command)
        {
            if (command.UnitPrefab == null)
            {
                Debug.Log("No prefab to produce unit");
                return;
            }

            var transform1 = transform;
            Instantiate(command.UnitPrefab, transform1.position, Quaternion.identity, transform1.parent);
        }
    }
}
