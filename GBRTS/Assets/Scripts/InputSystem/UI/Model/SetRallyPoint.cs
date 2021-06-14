using Abstractions;
using UnityEngine;

namespace InputSystem.UI.Model
{
    public class SetRallyPoint : ISetRallyPointCommand
    {
        public Vector3 RallyPoint { get; }

        public SetRallyPoint(Vector3 rallyPoint)
        {
            RallyPoint = rallyPoint;
        }
    }
}