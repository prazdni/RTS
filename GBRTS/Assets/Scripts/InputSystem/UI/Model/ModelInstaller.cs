using System;
using Abstractions;
using InputSystem.UI.Model;
using UnityEngine;
using Zenject;

namespace UI.Model
{
    public class ModelInstaller : MonoInstaller
    {
        [Inject] private AssetsContext _assetsContext;
        
        public override void InstallBindings()
        {
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<ISetRallyPointCommand>>().To<SetRallyPointCommandCreator>().AsTransient();

            Container.Bind<ButtonPanel>().AsSingle();
            Container.Bind<UnitProductionPanel>().AsSingle();
            
            SetUnitInfo();
        }

        private void SetUnitInfo()
        {
            Container.Bind<EllenProductionTime>().WithId("Ellen").FromInstance(new EllenProductionTime(100)).AsSingle();
            Container.Bind<EllenUnitSprite>().WithId("Ellen").FromInstance(new EllenUnitSprite(_assetsContext.GetSprite("Ellen"))).AsSingle();
            
            Container.Bind<ChomperProductionTime>().WithId("Chomper").FromInstance(new ChomperProductionTime(200)).AsSingle();
            Container.Bind<ChomperUnitSprite>().WithId("Chomper").FromInstance(new ChomperUnitSprite(_assetsContext.GetSprite("Chomper"))).AsSingle();
        }
    }
}