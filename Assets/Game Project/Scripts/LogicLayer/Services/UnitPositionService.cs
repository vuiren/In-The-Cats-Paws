using System.Collections.Generic;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Services
{
    internal sealed class UnitPositionService : IUnitPositionService
    {
        private readonly Dictionary<Unit, Vector3> _unitPositions = new();
        
        public void SetUnitPosition(Unit unitView, Vector3 position)
        {
            if (_unitPositions.ContainsKey(unitView))
            {
                _unitPositions[unitView] = position;
            }
            else
            {
                _unitPositions.Add(unitView, position);
            }
        }

        public Vector3 GetUnitPosition(Unit unitView)
        {
            if (_unitPositions.ContainsKey(unitView))
            {
                return _unitPositions[unitView];
            }

            return unitView.GameObjectLink.transform.position;
        }
    }
}