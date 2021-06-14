using Core.Unit;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackCommandExecutor>().FromComponentsInHierarchy().AsTransient();
        }
    }
}