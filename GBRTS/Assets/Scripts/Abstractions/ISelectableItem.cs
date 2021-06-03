using QuickOutline;
using UnityEngine;

namespace Abstractions
{
    public interface ISelectableItem : IHealthHolder
    {
        Transform UnitTransform { get; }
        Sprite Icon { get; }
        Outline Outline { get; }
    }
}