using System;
using Abstractions;
using Core.Unit;
using UnityEngine;

namespace Core
{
    public class WalkableUnit : UnitBase, IAttacker
    {
        public float VisionRange => _visionRange;
        [SerializeField] private float _visionRange;

        protected void Update()
        {
            Position = transform.position;
        }

        public void AutoAttackTarget(IAttackable target)
        {
            GetComponent<AttackCommandExecutor>().Execute(new AutoAttackCommand(target));
        }
    }
}