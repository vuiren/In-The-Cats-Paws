using System;
using System.Collections.Generic;
using Game_Code.MonoBehaviours.Players;
using Game_Code.MonoBehaviours.Units;

namespace Game_Code.Services
{
    public interface IUnitsSelectionService
    {
        void RegisterOnUnitSelection(Action<IUnit> action);
        void RegisterOnUnitDeselection(Action<IUnit> action);
        void SelectUnit(IUnit unit, Player player);
        void DeselectUnit(IUnit unit, Player player);
        IUnit GetPlayerSelectedUnit(Player player);
    }
    
    public class UnitsSelectionService: IUnitsSelectionService
    {
        private readonly Dictionary<Player, IUnit> _selectedUnit = new Dictionary<Player, IUnit>();
        private Action<IUnit> _onUnitSelected, _onUnitDeselected;
        private readonly ILogger _logger;
        
        public UnitsSelectionService(ILogger logger)
        {
            _logger = logger;
        }

        public void RegisterOnUnitSelection(Action<IUnit> action)
        {
            _onUnitSelected += action;
        }

        public void RegisterOnUnitDeselection(Action<IUnit> action)
        {
            _onUnitDeselected += action;
        }

        public void SelectUnit(IUnit unit, Player player)
        {
            _selectedUnit[player] = unit;
            _onUnitSelected?.Invoke(unit);
            _logger.Log($"Unit {unit.UnitGameObject().name} has been selected by {player.name}");
        }

        public void DeselectUnit(IUnit unit, Player player)
        {
            _selectedUnit[player] = null;
            _onUnitDeselected?.Invoke(unit);
            _logger.Log($"Unit {unit.UnitGameObject().name} has been deselected");
        }

        public IUnit GetPlayerSelectedUnit(Player player)
        {
            return _selectedUnit.ContainsKey(player) ? _selectedUnit[player] : null;
        }
    }
}