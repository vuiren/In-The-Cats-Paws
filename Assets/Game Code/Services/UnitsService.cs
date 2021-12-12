using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Units;

namespace Game_Code.Services
{
    public interface IUnitsService
    {
        Unit[] GetAll();
        void RegisterUnit(Unit unit, UnitType unitType);
        Unit GetUnitByName(string name);
        IEnumerable<Unit> GetUnitsByUnitType(UnitType unitType);
    }
    
    public class UnitsService: IUnitsService
    {
        private readonly Dictionary<UnitType, List<Unit>> _units = new();
        private readonly ILogger _logger;

        public UnitsService(ILogger logger)
        {
            _logger = logger;
        }

        public Unit[] GetAll()
        {
            return _units.SelectMany(x => x.Value).ToArray();
        }

        public void RegisterUnit(Unit unit, UnitType unitType)
        {
            _logger.Log($"Registering unit {unit.name}");
            
            if (_units.ContainsKey(unitType))
            {
                if (_units[unitType].All(x => x.gameObject.name != unit.name))
                {
                    _units[unitType].Add(unit);
                }
                else
                {
                    _logger.LogWarning($"Unit {unit.name} already registered");
                }
            }
            else
            {
                _units[unitType] = new List<Unit>() {unit};
            }
        }

        public Unit GetUnitByName(string name)
        {
            _logger.Log($"Searching for unit with name {name}");
            
            var possiblePairs = 
                _units.Where(valuePair => valuePair.Value.Any(unit => unit.gameObject.name == name));

            var possibleUnits = possiblePairs
                .Select(x => x.Value)
                .SelectMany(x => x)
                .ToArray();

            switch (possibleUnits.Count())
            {
                case > 1:
                    _logger.LogWarning($"Found several units with name {name}");
                    return possibleUnits[0];
                case 1:
                    return possibleUnits.FirstOrDefault();
                default:
                 //   _logger.LogError($"No unit with name {name} has been found");
                    throw new Exception($"No unit with name {name} has been found");
            }
        }

        public IEnumerable<Unit> GetUnitsByUnitType(UnitType unitType)
        {
            _logger.Log($"Getting units of type {unitType}");
            
            if (_units.ContainsKey(unitType))
            {
                return _units[unitType].ToArray();
            }

            // _logger.LogWarning($"No units type of {unitType} found");
            throw new Exception($"No units type of {unitType} found");
        }
    }
}