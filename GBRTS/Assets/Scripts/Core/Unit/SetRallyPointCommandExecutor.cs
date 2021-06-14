using System.Threading.Tasks;
using Abstractions;

namespace Core.Unit
{
    public class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override Task ExecuteConcreteCommand(ISetRallyPointCommand command)
        {
            return Task.Run(() => GetComponent<MainBuilding>().RallyPoint = command.RallyPoint);
        }
    }
}