using System;
using Abstractions;
using UI.Model;
using UnityEngine;

namespace InputSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Strategy/" + nameof(AttackableValue))]
    public class AttackableValue : StatelessScriptableObjectBase<IAttackable>
    {
        
    }
}