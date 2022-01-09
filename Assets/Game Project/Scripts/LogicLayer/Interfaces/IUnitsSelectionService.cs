using System;
using Game_Project.Scripts.DataLayer.Units;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IUnitsSelectionService
    {
        void RegisterOnUnitSelection(Action<Unit> action);
        void RegisterOnUnitDeselection(Action<Unit> action);
        void SelectUnit(int unitId);
        void DeselectUnit();
        Unit GetSelectedUnit();
    }
}