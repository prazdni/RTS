using Abstractions;
using Core.Unit;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private GameStatus _gameStatus;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackCommandExecutor>().FromComponentsInHierarchy().AsTransient();
            Container.Bind<IGameStatus>().FromInstance(_gameStatus);
        }
    }
}