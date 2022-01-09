using Game_Project.Scripts.DataLayer.Level;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IExitPointsService
    {
        void RegisterExitPoint(ExitPoint exitPoint);
        ExitPoint GetExitPoint();
    }
}