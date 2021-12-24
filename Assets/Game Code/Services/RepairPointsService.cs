using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Level;

namespace Game_Code.Services
{
    public interface IRepairPointsService
    {
        void AddRepairPoint(Room room, RepairPoint repairPoint);
        RepairPoint GetRepaintPointInRoom(Room room);
        RepairPoint[] GetAll();
    }
    
    public class RepairPointsService: IRepairPointsService
    {
        private readonly Dictionary<Room, RepairPoint> _repairPoints;

        public RepairPointsService(SceneData sceneData)
        {
            _repairPoints = sceneData.repairPoints.ToDictionary(x => x.room, x => x);
        }
        
        public void AddRepairPoint(Room room, RepairPoint repairPoint)
        {
            _repairPoints.Add(room, repairPoint);
        }

        public RepairPoint GetRepaintPointInRoom(Room room)
        {
            return _repairPoints.ContainsKey(room) ? _repairPoints[room] : null;
        }

        public RepairPoint[] GetAll()
        {
            return _repairPoints.Select(x => x.Value).ToArray();
        }
    }
}