using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Core.Behaviour;
using UnityEngine;

namespace Core
{
    public class FractionMember : MonoBehaviour, IFractionMember
    {
        public static int FractionsCount
        {
            get
            {
                lock (_membersCount)
                {
                    return _membersCount.Count;
                }
            }
        }
        private static Dictionary<int, List<int>> _membersCount = new Dictionary<int, List<int>>();

        public int Id => _id;
        [SerializeField] private int _id;

        protected void Awake()
        {
            if (_id != 0)
            {
                Register();
            }
        }

        private void Register()
        {
            lock (_membersCount)
            {
                if (!_membersCount.ContainsKey(_id))
                    _membersCount.Add(_id, new List<int>());
                if (!_membersCount[_id].Contains(GetInstanceID()))
                    _membersCount[_id].Add(GetInstanceID());
            }
        }
        
        private void Unregister()
        {
            lock (_membersCount)
            {
                if (_membersCount[_id].Contains(GetInstanceID()))
                    _membersCount[_id].Remove(GetInstanceID());
                if (_membersCount[_id].Count == 0)
                    _membersCount.Remove(_id);
            }
        }
        
        public void SetFaction(int factionId)
        {
            _id = factionId;
            Register();
        }


        protected void Start()
        {
            AutoAttackGlobalBehavior.Units.AddOrUpdate(gameObject.GetComponent<WalkableUnit>(), this, (u, fraction) => fraction);
        }

        protected void OnDestroy()
        {
            AutoAttackGlobalBehavior.Units.TryRemove(gameObject.GetComponent<WalkableUnit>(), out var fractionMember);
            Unregister();
        }

        public static int GetWinner()
        {
            lock (_membersCount)
            {
                return _membersCount.Keys.First();
            }
        }
    }
}