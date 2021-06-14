using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace Core.Unit
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override Task ExecuteConcreteCommand(IAttackCommand command)
        {
            return Task.Run(() =>
            {
                Debug.Log(name + "is attacking!");
            });
        }
    }
}