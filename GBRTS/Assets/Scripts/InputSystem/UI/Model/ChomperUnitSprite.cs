using Abstractions;
using UnityEngine;

namespace InputSystem.UI.Model
{
    public class ChomperUnitSprite : IUnitSprite
    {
        public Sprite Sprite { get; }

        public ChomperUnitSprite(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}