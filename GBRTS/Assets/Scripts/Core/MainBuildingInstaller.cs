using Abstractions;
using Zenject;

namespace Core
{
    public class MainBuildingInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProduceUnitExecutorEllen>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<ProduceUnitExecutorChomper>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<CommandExecutorBase<ISetRallyPointCommand>>().FromComponentsInHierarchy().AsTransient();
        }
    }
}