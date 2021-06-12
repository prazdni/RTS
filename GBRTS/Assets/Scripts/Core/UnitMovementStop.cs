using System;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter();

        private class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
        }
    }
}