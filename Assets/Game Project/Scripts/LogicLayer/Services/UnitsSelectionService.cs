using System;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class UnitsSelectionService: IUnitsSelectionService
    {
        private Unit _selectedUnit;
        private readonly IUnitsService _unitsService;
        private Action<Unit> _onUnitSelected, _onUnitDeselected;
        private readonly IMyLogger _logger;

        public UnitsSelectionService(IUnitsService unitsService)
        {
            _logger = LoggerFactory.Create(this);
            _unitsService = unitsService;
        }

        public void RegisterOnUnitSelection(Action<Unit> action)
        {
            _onUnitSelected += action;
        }

        public void RegisterOnUnitDeselection(Action<Unit> action)
        {
            _onUnitDeselected += action;
        }

        public void SelectUnit(int unitId)
        {
            if(_selectedUnit != null)
                DeselectUnit();

            _logger.Log($"Selecting unit with id {unitId}");
            var unit = _unitsService.GetUnitByID(unitId);
            _selectedUnit = unit;
            _onUnitSelected?.Invoke(unit);
            _logger.Log($"Unit '{unit}' has been selected");
        }

        public void DeselectUnit()
        {
            if(_selectedUnit == null) return;
            
            var unit = _selectedUnit;
            _selectedUnit = null;
            _onUnitDeselected?.Invoke(unit);
            _logger.Log($"Unit {unit.GameObjectLink.name} has been deselected");
        }

        public Unit GetSelectedUnit() => _selectedUnit;
    }
}