using System;
using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public class UnitsService: IUnitsService
    {
        private readonly Dictionary<int, Unit> _units = new();
        private readonly IMyLogger _logger;
        private readonly IRoomsService _roomsService;

        private Action<Unit> _onUnitRegistered;

        [Inject]
        public UnitsService(IRoomsService roomsService)
        {
            _logger = LoggerFactory.Create(this);
            _roomsService = roomsService;
        }

        public Unit[] GetAll()
        {
            return _units.Select(x => x.Value).ToArray();
        }

        public void RegisterUnit(Unit unit)
        {
            var id = unit.ID;
            var unitType = unit.UnitType;
            _logger.Log($"Registering unit {unitType} with id {id}");
            
            if (_units.ContainsKey(id))
            {
                _logger.LogError($"{unitType} with id {id} already exist");
            }
            else
            {
                _units[id] = unit;
                _onUnitRegistered?.Invoke(unit);
                _logger.Log($"Done registering unit {unitType}");
            }
        }

        public Unit GetUnitByID(int id)
        {
            if (_units.ContainsKey(id))
            {
                return _units[id];
            }
            else
            {
                throw new Exception($"Unit with id {id} is not exist");
            }
        }

        public void UnitGoToRoom(int unitId, Vector2Int roomCoords)
        {
            _units[unitId].Room = roomCoords;
            _units[unitId].Position = _roomsService.GetPlaceInRoom(roomCoords);
        }

        IEnumerable<Unit> IUnitsService.GetUnitsByUnitType(UnitType unitType)
        {
            return _units.Select(x => x.Value).Where(x => x.UnitType == unitType);
        }

        public void RegisterOnUnitRegistration(Action<Unit> action)
        {
            _onUnitRegistered += action;
        }
    }
}