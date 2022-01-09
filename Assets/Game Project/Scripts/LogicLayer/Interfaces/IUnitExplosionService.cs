using System;
using Game_Project.Scripts.DataLayer.Units;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IUnitExplosionService
    {
        Tuple<int, int>[] GetAll();
        void RegisterUnitForExplosion(Unit unit, int turnsUntilExplosion);
        void OnUnitExplosionStart(Action<Unit> action);
        int TurnLeftUntilUnitExplode(int unitId);
        void TickTimer();
        void OnUnitExplosion(Action<Unit> action);
    }
}