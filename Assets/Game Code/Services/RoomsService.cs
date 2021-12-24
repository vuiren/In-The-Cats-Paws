using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Level;

namespace Game_Code.Services
{
    public interface IRoomsService
    {
        Room[] GetAll();
        Room GetRoomById(int roomId);
        int GetRoomId(Room room);
    }

    public class RoomsService: IRoomsService
    {
        private readonly Dictionary<int, Room> _map;
        private readonly ILogger _logger;
        
        public RoomsService(ILogger logger, SceneData sceneData)
        {
            _logger = logger;
            _map = sceneData.database.Rooms;
        }

        public Room[] GetAll()
        {
            return _map.Select(x => x.Value).ToArray();
        }

        public Room GetRoomById(int id)
        {
            _logger.Log(this,$"Getting room with id {id}");

            if (_map.ContainsKey(id))
            {
                var room = _map[id];
                _logger.Log(this,$"Found room {room.gameObject.name}");
                return room;
            }

            _logger.LogError($"Room with id {id} has not been found");
            throw new Exception($"Room with id {id} has not been found");
        }

        public int GetRoomId(Room room)
        {
            return _map.First(x => x.Value == room).Key;
        }
    }
}