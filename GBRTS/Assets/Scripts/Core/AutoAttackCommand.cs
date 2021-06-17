using Abstractions;

namespace Core
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AutoAttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}