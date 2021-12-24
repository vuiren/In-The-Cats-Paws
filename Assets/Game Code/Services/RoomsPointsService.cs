
using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Level;
using UnityEngine;

namespace Game_Code.Services
{
    public interface IRoomsPointsService
    {
        Vector3 GetFreePointFromRoom(Room room);
        void AddFreePointToRoom(Room room, Vector3 point);
    }
    
    public class RoomsPointsService: IRoomsPointsService
    {
        private IRoomsService _roomsService;
        private readonly Dictionary<Room, Queue<Vector3>> _pointsInRooms;

        public RoomsPointsService(IRoomsService roomsService)
        {
            _pointsInRooms = roomsService
                .GetAll()
                .ToDictionary(x => x, x => new Queue<Vector3>(x.GetFreePoints()));
        }

        public Vector3 GetFreePointFromRoom(Room room)
        {
            if (_pointsInRooms.ContainsKey(room) && _pointsInRooms[room].Count > 0)
            {
                return _pointsInRooms[room].Dequeue();
            }

            throw new Exception($"No free points in room {room.name}");
        }

        public void AddFreePointToRoom(Room room, Vector3 point)
        {
            _pointsInRooms[room].Enqueue(point);
        }
    }
}