using Abstractions;
using UnityEngine;

namespace Core
{
    public class MoveCommand : IMoveCommand
    {
        public Vector3 Position { get; }

        public MoveCommand(Vector3 position)
        {
            Position = position;
        }
    }
}