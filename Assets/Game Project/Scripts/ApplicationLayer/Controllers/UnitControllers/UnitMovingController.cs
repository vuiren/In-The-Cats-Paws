using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.UnitControllers
{
    public sealed class UnitMovingController
    {
        private readonly IMyLogger _logger;
        private readonly IUnitsService _unitsService;
        private readonly ICorridorsService _corridorsService;
        private readonly IRoomsService _roomsService;

        public UnitMovingController(IUnitsService unitsService, ICorridorsService corridorsService,
            IRoomsService roomsService)
        {
            _logger = LoggerFactory.Create(this);
            _unitsService = unitsService;
            _roomsService = roomsService;
            _corridorsService = corridorsService;
        }

        public bool TryToSendUnitToRoom(int unitId, Vector2Int room)
        {
            var unit = _unitsService.GetUnitByID(unitId);
            var isCorridorAvailable = CanUnitGoToRoom(unit, room);
            if (isCorridorAvailable)
            {
                _logger.Log($"Unit {unit} can go to room {room}");
                _roomsService.ReturnPlaceInRoom(unit.Room, unit.Position);
                _unitsService.UnitGoToRoom(unit.ID, room);
                unit.GameObjectLink.GetComponent<UnitView>().model = _unitsService.GetUnitByID(unit.ID);
                return true;
            }
            else
            {
                _logger.LogWarning($"Unit {unit} can't go to room {room}");
                return false;
            }
        }

        private bool CanUnitGoToRoom(Unit unit, Vector2Int room)
        {
            var corridor = _corridorsService.GetCorridor(unit.Room, room);

            if (corridor == null)
            {
                _logger.LogWarning($"There is no corridor between rooms {unit.Room} and {room}");
                return false;
            }

            if (unit.Room == room)
            {
                _logger.LogWarning("Unit can't go in the same room");
                return false;
            }

            return !corridor.Locked;
        }
    }
}