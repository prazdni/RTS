using System;
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
        Container.BindInstances(_assetsContext, _currentSelection, _currentGroundPosition, _currentAttackableValue);
    }
}