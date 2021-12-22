using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Units;

namespace Game_Code.Controllers
{
    public interface IUnitsCommandGiver
    {
        void GoToRoom(Unit unit, Room room);
    }
    
    public class UnitsCommandsGiver: IUnitsCommandGiver
    {
        public void GoToRoom(Unit unit, Room room)
        {
            
        }
    }
}