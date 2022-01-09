using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public class EngineerRepairPointFixer
    {
        private readonly IMyLogger _logger;
        private readonly IRepairPointsService _repairPointsService;
        private readonly IUnitsService _unitsService;

        public EngineerRepairPointFixer(IRepairPointsService repairPointsService, IUnitsService unitsService)
        {
            _logger = LoggerFactory.Create(this);
            _repairPointsService = repairPointsService;
            _unitsService = unitsService;
        }
        
        public void TryToFixRepairPointAtRoom(int unitId, Vector2Int room)
        {
            var engineer = _unitsService.GetUnitByID(unitId);
            if (engineer == null)
            {
                _logger.LogWarning($"Unit with id {unitId} not found");
                return;
            }

            if (engineer.UnitType != UnitType.Engineer)
            {
                _logger.LogWarning($"Unit type of {engineer.UnitType} can't fix");
                return;
            }

            if (engineer.Room != room)
            {
                _logger.LogWarning("Unit must be in the room with a repair point.");
                _logger.LogWarning($"Unit room {engineer.Room} Repair Point Room {room}");
                return;
            }
            
            var repairPoint = _repairPointsService.GetRepaintPointInRoom(room);

            if (repairPoint == null)
            {
                _logger.LogWarning($"Repair point at room {room} not found");
                return;
            }

            _repairPointsService.FixRepairPoint(repairPoint);
        }
    }
}