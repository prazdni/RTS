using Abstractions;

namespace InputSystem.UI.Model
{
    public class EllenProductionTime : IProductionTime
    {
        public int Time { get; }
        
        public EllenProductionTime(int time)
        {
            Time = time;
        }
    }
}