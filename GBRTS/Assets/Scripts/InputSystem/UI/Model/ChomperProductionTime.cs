using Abstractions;

namespace InputSystem.UI.Model
{
    public class ChomperProductionTime : IProductionTime
    {
        public int Time { get; }
        
        public ChomperProductionTime(int time)
        {
            Time = time;
        }
    }
}