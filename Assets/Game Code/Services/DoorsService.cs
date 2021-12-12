using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Level;

namespace Game_Code.Services
{
    public interface IDoorsService
    {
        void SwitchDoorState(int doorId);
        int GetDoorId(Door door);
    }

    public class DoorsService : IDoorsService
    {
        private readonly Dictionary<int, Door> _doors;

        public DoorsService(SceneData sceneData)
        {
            _doors = sceneData.database.Doors;
        }

        public void SwitchDoorState(int doorId)
        {
            if (_doors.ContainsKey(doorId))
            {
                _doors[doorId].SwitchDoorState();
            }
        }

        public int GetDoorId(Door door)
        {
            return _doors.First(x => x.Value == door).Key;
        }
    }
}