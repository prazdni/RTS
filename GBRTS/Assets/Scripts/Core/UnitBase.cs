using Abstractions;
using QuickOutline;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Outline))]
    public class UnitBase : MonoBehaviour, ISelectableItem, IAttackable
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Outline _outline;
        public Transform UnitTransform => transform;
        public Vector3 Position => transform.position;
        public Sprite Icon => _icon;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Outline Outline => _outline;
        
        public void ReceiveDamage(float value)
        {
            _health -= value;
            if (_health <= 0)
            {
                Debug.Log("Unit Dead");
                Destroy(gameObject);
            }
        }
    }
}