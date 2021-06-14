using UnityEngine;

namespace Abstractions
{
    public interface IAttackable : IHealthHolder
    {
        Vector3 Position { get; }
        void ReceiveDamage(float value);
    }
}