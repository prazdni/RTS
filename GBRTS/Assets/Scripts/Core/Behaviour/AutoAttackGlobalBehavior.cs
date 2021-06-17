using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using UniRx;
using UnityEngine;

namespace Core.Behaviour
{
    public class AutoAttackGlobalBehavior : MonoBehaviour
    {
        private struct AutoAttackCommandInfo
        {
            public WalkableUnit Attacker;
            public IAttackable Target;

            public AutoAttackCommandInfo(WalkableUnit attacker, IAttackable target)
            {
                Attacker = attacker;
                Target = target;
            }
        }
        public static ConcurrentDictionary<WalkableUnit, FractionMember> Units = new ConcurrentDictionary<WalkableUnit, FractionMember>();
        private static Subject<AutoAttackCommandInfo> _attackTarget = new Subject<AutoAttackCommandInfo>();

        protected void Start()
        {
            _attackTarget.ObserveOnMainThread().Subscribe(PerformAutoAttack).AddTo(this);
        }

        private void PerformAutoAttack(AutoAttackCommandInfo commandInfo)
        {
            if (commandInfo.Attacker != null)
            {
                commandInfo.Attacker.AutoAttackTarget(commandInfo.Target);
            }
        }

        protected void Update()
        {
            Parallel.ForEach(Units, AttackNearTarget);
        }

        private void AttackNearTarget(KeyValuePair<WalkableUnit, FractionMember> unitWithInfo)
        {
            var unit = unitWithInfo.Key;
            var fraction = unitWithInfo.Value;

            foreach (var kvp in Units)
            {
                if (kvp.Value.Id == fraction.Id)
                {
                    continue;
                }

                var otherUnit = kvp.Key;
                var distance = (unit.Position - kvp.Key.Position).magnitude;
                if (distance < unit.VisionRange)
                {
                    _attackTarget.OnNext(new AutoAttackCommandInfo(unit, otherUnit));
                    break;
                }
            }
        }
    }
}