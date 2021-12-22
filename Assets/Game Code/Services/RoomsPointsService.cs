
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Level;
using UnityEngine;

namespace Game_Code.Services
{
    public interface IRoomsPointsService
    {
        Vector3 GetFreePointForRoom(Room room);
        void AddFreePointToRoom(Vector3 point);
    }
    
    public class RoomsPointsService: IRoomsPointsService
    {
        private IRoomsService _roomsService;
        private Dictionary<Room, Queue<Vector3>> _pointsInRooms;

        public RoomsPointsService(IRoomsService roomsService)
        {
            _pointsInRooms = roomsService
                .GetAll()
                .ToDictionary(x => x, x => new Queue<Vector3>(x.GetFreePoints()));
        }

        public Vector3 GetFreePointForRoom(Room room)
        {
            if (_pointsInRooms.ContainsKey(room))
            {
                return 
            }
        }

        public void AddFreePointToRoom(Vector3 point)
        {
            throw new System.NotImplementedException();
        }
    }
}