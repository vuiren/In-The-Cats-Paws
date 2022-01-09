using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public class RepairPointsService: IRepairPointsService
    {
        private readonly Dictionary<Vector2Int, RepairPoint> _repairPoints = new();
        private IMyLogger _logger;

        [Inject]
        public RepairPointsService()
        {
            _logger = LoggerFactory.Create(this);
        }
        
        public void RegisterRepairPoint(RepairPoint repairPoint)
        {
            if (_repairPoints.ContainsKey(repairPoint.Room))
            {
                _logger.LogWarning($"{repairPoint.ToString()} already registered");
            }
            
            _repairPoints.Add(repairPoint.Room, repairPoint);
        }

        public RepairPoint GetRepaintPointInRoom(Vector2Int room)
        {
            if (!_repairPoints.ContainsKey(room))
            {
                _logger.LogWarning($"Repair point in room {room} has not been found");
                return null;
            }

            return _repairPoints[room];
        }

        public RepairPoint[] GetAll()
        {
            return _repairPoints.Values.ToArray();
        }

        public void FixRepairPoint(RepairPoint repairPoint)
        {
            repairPoint.TurnsLeftToFix--;
            repairPoint.PointFixed = repairPoint.TurnsLeftToFix <= 0;
        }
    }
}