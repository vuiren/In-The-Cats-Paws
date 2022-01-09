using Game_Project.Scripts.DataLayer.Units;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IUnitPositionService
    {
        void SetUnitPosition(Unit unitView, Vector3 position);
        Vector3 GetUnitPosition(Unit unitView);
    }
}