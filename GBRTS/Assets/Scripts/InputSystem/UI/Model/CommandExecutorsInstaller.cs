using Abstractions;
using UnityEngine;
using Zenject;

namespace InputSystem.UI.Model
{
    public class CommandExecutorsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var executors = gameObject.GetComponentsInChildren<ICommandExecutor>();
            foreach (var executor in executors)
            {
                var baseType = executor.GetType().BaseType;
                Container.Bind(baseType).FromInstance(executor);
            }
        }
    }
}