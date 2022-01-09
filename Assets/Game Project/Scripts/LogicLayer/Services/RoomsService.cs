using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class RoomsService: IRoomsService
    {
        private readonly Dictionary<Vector2Int, Room> _map;
        private readonly IMyLogger _logger;
        
        public RoomsService(IEnumerable<Room> rooms)
        {
            _logger = LoggerFactory.Create(this);
            _map = rooms.ToDictionary(x => x.Coords, x => x);
        }

        public IEnumerable<Room> GetAll() => _map.Values.ToArray();

        public Room GetRoomByCoord(Vector2Int coord)
        {
            if (_map.ContainsKey(coord))
            {
                return _map[coord];
            }
            
            _logger.LogWarning($"Room at {coord} has not been found");
            return null;
        }

        public void RegisterRoom(Room room)
        {
            if (_map.ContainsKey(room.Coords))
            {
                _logger.LogWarning($"Room with id {room.ID} already registered");
            }
            else
            {
                _map.Add(room.Coords, room);
            }
        }

        public Vector3 GetPlaceInRoom(Vector2Int room)
        {
            var freePoints = new Queue<Vector3>(_map[room].FreePoints);

            var freePoint = freePoints.Dequeue();

            _map[room].FreePoints = freePoints.ToList();
            
            return freePoint;
        }

        public void ReturnPlaceInRoom(Vector2Int room, Vector3 point)
        {
            _map[room].FreePoints.Add(point);
        }
    }
}