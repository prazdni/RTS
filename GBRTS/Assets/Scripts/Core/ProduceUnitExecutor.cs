using System.Linq;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ProduceUnitExecutor : CommandExecutorBase<IProduceUnitCommand>, ITickable, IUnitProducer
    {
        public IReactiveCollection<IUnitProductionTask> Queue => _queue;
        private IReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        protected override void ExecuteConcreteCommand(IProduceUnitCommand command)
        {
            if (command.UnitPrefab == null)
            {
                Debug.Log("No prefab to produce unit");
                return;
            }

            _queue.Add(new UnitProductionTask(command.ProductionIcon, command.ProductionTime, command.UnitPrefab));
        }

        public void Tick()
        {
            if (_queue.Count == 0)
            {
                return;
            }
            
            var currentTask = _queue[0];
            currentTask.ProductionTimeLeft -= Mathf.Min(Time.deltaTime, currentTask.ProductionTimeLeft);

            if (currentTask.ProductionTimeLeft <= 0)
            {
                CreateUnit(currentTask);
                Queue.Remove(currentTask);
            }
        }

        private void CreateUnit(IUnitProductionTask unitProductionTask)
        {
            var transform1 = transform;
            Instantiate(unitProductionTask.UnitPrefab, transform1.position + Vector3.forward * 5, Quaternion.identity, transform1.parent);
        }
    }
}