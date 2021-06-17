using System.Linq;
using System.Threading.Tasks;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public abstract class ProduceUnitExecutorBase<T> : CommandExecutorBase<T>, ITickable, IUnitProducer where T : class, IProduceUnitCommand
    {
        public IReactiveCollection<IUnitProductionTask> Queue => _queue;
        private IReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
        private DiContainer _diContainer = new DiContainer();

        public override Task ExecuteConcreteCommand(T command)
        {
            return Task.Run(() =>
            {
                if (command.UnitPrefab == null)
                {
                    Debug.Log("No prefab to produce unit");
                }
                else
                {
                    _queue.Add(new UnitProductionTask(command.ProductionIcon.Sprite, command.ProductionTime.Time, command.UnitPrefab));
                }
            });
            
        }

        public void Tick()
        {
            if (_queue.Count == 0)
            {
                return;
            }
            
            var currentTask = _queue[0];
            currentTask.ProductionTimeLeft.Value -= Mathf.Min(Time.deltaTime, currentTask.ProductionTimeLeft.Value);

            if (currentTask.ProductionTime <= 0)
            {
                CreateUnit(currentTask);
                Queue.Remove(currentTask);
            }
        }

        private void CreateUnit(IUnitProductionTask unitProductionTask)
        {
            var go = _diContainer.InstantiatePrefab(unitProductionTask.UnitPrefab, transform.position, Quaternion.identity, transform);
            var queue = go.GetComponent<ICommandsQueue>();
            var mainBuilding = GetComponent<MainBuilding>();
            queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));
        }
    }
}