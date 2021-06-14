using UniRx;
using UnityEngine;

namespace System
{
    public class CollisionDetector : MonoBehaviour
    {
        public IObservable<Collision> Collisions => _collisions;
        private Subject<Collision> _collisions = new Subject<Collision>();

        private void OnCollisionStay(Collision other)
        {
            _collisions.OnNext(other);
        }
    }
}