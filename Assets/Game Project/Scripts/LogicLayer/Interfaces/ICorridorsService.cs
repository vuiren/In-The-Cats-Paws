using System.Collections.Generic;
using Game_Project.Scripts.DataLayer.Level;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface ICorridorsService
    {
        IEnumerable<Corridor> GetAll();
        void RegisterCorridor(Corridor corridor);
        void LockCorridor(Vector2Int room1, Vector2Int room2, bool doLock);
        Corridor GetCorridor(Vector2Int room1, Vector2Int room2);
    }
}