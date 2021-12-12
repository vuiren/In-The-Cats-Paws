using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Units;

namespace Game_Code.Services
{
    public interface IUnitRoomService
    {
        List<Unit> GetAllUnitsInRoom(int roomId);
        List<Unit> GetAllUnitsInRoom(Room room);
        Room FindUnitRoom(Unit unit);
        bool TryToAddUnitToRoom(string unitName, int roomId);
        void RemoveUnitFromRoom(string unitName, int roomId);
        bool CanUnitGoToRoom(Unit unit, Room targetRoom);
    }

    public class UnitRoomService : IUnitRoomService
    {
        private readonly Dictionary<int, List<Unit>> _unitsInRoom = new(); // int - room id

        private readonly IRoomsService _roomsService;
        private readonly IUnitsService _unitsService;
        private readonly ILogger _logger;

        public UnitRoomService(IRoomsService roomsService, IUnitsService unitsService, ILogger logger)
        {
            _roomsService = roomsService;
            _unitsService = unitsService;
            _logger = logger;
        }

        public List<Unit> GetAllUnitsInRoom(int roomId)
        {
            return _unitsInRoom.ContainsKey(roomId) ? _unitsInRoom[roomId] : new List<Unit>();
        }

        public List<Unit> GetAllUnitsInRoom(Room room)
        {
            var roomId = _roomsService.GetRoomId(room);
            return _unitsInRoom.ContainsKey(roomId) ? _unitsInRoom[roomId] : new List<Unit>();
        }

        public Room FindUnitRoom(Unit unit)
        {
            var possibleRooms = _unitsInRoom.Where(x => x.Value.Contains(unit));
            var room = _roomsService.GetRoomById(possibleRooms.First().Key);
            return room;
        }

        public bool TryToAddUnitToRoom(string unitName, int roomId)
        {
            try
            {
                var unit = _unitsService.GetUnitByName(unitName);
                var room = _roomsService.GetRoomById(roomId);

                if (_unitsInRoom.ContainsKey(roomId))
                {
                    _unitsInRoom[roomId].Add(unit);
                }
                else
                {
                    _unitsInRoom[roomId] = new List<Unit>() {unit};
                }

                return true;
            }
            catch (Exception)
            {
                _logger.LogError($"Couldn't register unit {unitName} to room with id {roomId}");
                return false;
            }
        }

        public void RemoveUnitFromRoom(string unitName, int roomId)
        {
            try
            {
                var unit = _unitsService.GetUnitByName(unitName);
                var room = _roomsService.GetRoomById(roomId);

                if (_unitsInRoom.ContainsKey(roomId))
                {
                    _unitsInRoom[roomId].Remove(unit);
                }
                else
                {
                    _logger.LogWarning($"No room with units with id {roomId} was found");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Couldn't remove unit {unitName} from room with id {roomId}");
            }
        }

        public bool CanUnitGoToRoom(Unit unit, Room targetRoom)
        {
            var unitRoom = FindUnitRoom(unit);

            return unitRoom.GetAvailableRooms().Any(x => targetRoom);
        }
    }
}