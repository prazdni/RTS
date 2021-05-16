using QuickOutline;
using UnityEngine;

namespace Abstractions
{
    public interface ISelectableItem
    {
        Sprite Icon { get; }
        float Health { get; }
        float MaxHealth { get; }
        Outline Outline { get; }
    }
}