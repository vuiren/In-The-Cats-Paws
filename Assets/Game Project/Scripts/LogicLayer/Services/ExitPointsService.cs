using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public class ExitPointsService: IExitPointsService
    {
        private ExitPoint _exitPoint;
        
        public void RegisterExitPoint(ExitPoint exitPoint)
        {
            _exitPoint = exitPoint;
        }

        public ExitPoint GetExitPoint()
        {
            return _exitPoint;
        }
    }
}