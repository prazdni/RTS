using System;
using Abstractions;
using UI.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace InputSystem.UI.Model
{
    public class UnitProductionPanel
    {
        public IReactiveCollection<IUnitProductionTask> UnitProductionQueue = new ReactiveCollection<IUnitProductionTask>();
        
        public void HandleSelectionChanged(ISelectableItem currentItem)
        {
            var unitProducer = (currentItem as Component)?.GetComponentInParent<IUnitProducer>();

            UnitProductionQueue = unitProducer?.Queue;
        }
    }
}