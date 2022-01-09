using System;
using System.Collections.Generic;
using Game_Project.Scripts.DataLayer.Units;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IUnitsService
    {
        Unit[] GetAll();
        void RegisterUnit(Unit unit);
        Unit GetUnitByID(int id);
        void UnitGoToRoom(int unitId, Vector2Int room);
        IEnumerable<Unit> GetUnitsByUnitType(UnitType unitType);
        void RegisterOnUnitRegistration(Action<Unit> action);
    }
}