using Abstractions;
using UnityEngine;

namespace Core
{
    public class FractionMember : MonoBehaviour, IFractionMember
    {
        public int Id => _id;
        [SerializeField] private int _id;
    }
}