using System;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Units
{
    [Serializable]
    public sealed class Unit : EntityModel
    {
        [SerializeField] private Vector2Int room;
        [SerializeField] private UnitType unitType;
        [SerializeField] private Vector3 position;

        public Vector2Int Room
        {
            get => room;
            set => room = value;
        }
        
        public UnitType UnitType
        {
            get => unitType;
            set => unitType = value;
        }

        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        public override string ToString()
        {
            return $"Unit type of {unitType} in room {room} with id {ID}";
        }
    }
}