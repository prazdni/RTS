using Abstractions;
using QuickOutline;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Outline))]
    public class SelectableItemCharacteristics : MonoBehaviour, ISelectableItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Outline _outline;
        
        public Sprite Icon => _icon;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Outline Outline => _outline;
    }
}