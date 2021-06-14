using Abstractions;
using UnityEngine;

namespace InputSystem.UI.Model
{
    public class EllenUnitSprite : IUnitSprite
    {
        public Sprite Sprite { get; }

        public EllenUnitSprite(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}