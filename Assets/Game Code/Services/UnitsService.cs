using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Units;

namespace Game_Code.Services
{
    public interface IUnitsService
    {
        IUnit[] GetAll();
        void RegisterUnit(IUnit unit, UnitType unitType);
        IUnit GetUnitByName(string name);
        IEnumerable<IUnit> GetUnitsByUnitType(UnitType unitType);
    }
    
    public class UnitsService: IUnitsService
    {
        private readonly Dictionary<UnitType, List<IUnit>> _units = new();
        private readonly ILogger _logger;

        public UnitsService(ILogger logger)
        {
            _logger = logger;
        }

        public IUnit[] GetAll()
        {
            return _units.SelectMany(x => x.Value).ToArray();
        }

        public void RegisterUnit(IUnit unit, UnitType unitType)
        {
            _logger.Log(this,$"Registering unit {unit.UnitGameObject()}");
            
            if (_units.ContainsKey(unitType))
            {
                if (_units[unitType].All(x => x.UnitGameObject().name != unit.UnitGameObject().name))
                {
                    _logger.Log(this,$"Done registering unit {unit.UnitGameObject()}");
                    _units[unitType].Add(unit);
                }
                else
                {
                    _logger.LogWarning($"Unit {unit.UnitGameObject().name} already registered");
                }
            }
            else
            {
                _units[unitType] = new List<IUnit>() {unit};
            }
        }

        public IUnit GetUnitByName(string name)
        {
            _logger.Log(this,$"Searching for unit with name {name}");
            
            var possiblePairs = 
                _units.Where(valuePair => valuePair.Value.Any(unit => unit.UnitGameObject().name == name));

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
                    _logger.Log(this,$"Found unit with name {name}");
                    return possibleUnits.FirstOrDefault();
                default:
                 //   _logger.LogError($"No unit with name {name} has been found");
                    throw new Exception($"No unit with name {name} has been found");
            }
        }

        public IEnumerable<IUnit> GetUnitsByUnitType(UnitType unitType)
        {
            _logger.Log(this,$"Getting units of type {unitType}");

            if (!_units.ContainsKey(unitType)) throw new Exception($"No units type of {unitType} found");
            _logger.Log(this,$"Found unit of type {unitType}");
            return _units[unitType].ToArray();

        }
    }
}