using System;
using Abstractions;
using InputSystem.UI.Model;
using UI.Model;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _assetsContext;
    [SerializeField] private SelectedItem _currentSelection;
    [SerializeField] private Vector3Value _currentGroundPosition;
    [SerializeField] private AttackableValue _currentAttackableValue;
    
    public override void InstallBindings()
    {
        Container.Bind<AssetsContext>().FromInstance(_assetsContext).AsSingle();
        Container.BindInterfacesAndSelfTo<Vector3Value>().FromInstance(_currentGroundPosition).AsSingle();
        Container.BindInterfacesAndSelfTo<AttackableValue>().FromInstance(_currentAttackableValue).AsSingle();
        Container.BindInterfacesAndSelfTo<SelectedItem>().FromInstance(_currentSelection).AsSingle();
    }
}