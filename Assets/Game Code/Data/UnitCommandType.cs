using Game_Code.MonoBehaviours.Units;
using Photon.Realtime;

namespace Game_Code.Data
{
    public interface IUnitCommand
    {
        void GoToRoom(Unit unit, Room room);
    }
}