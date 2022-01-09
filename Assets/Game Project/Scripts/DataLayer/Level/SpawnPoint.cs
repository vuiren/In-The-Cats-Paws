using System;
using Game_Project.Scripts.DataLayer.Units;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Level
{
    [Serializable]
    public class SpawnPoint: EntityModel
    {
        [SerializeField] private Vector2Int room;
        [SerializeField] private UnitType unitType;

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

        public override string ToString()
        {
            return $"Spawn point at room {room} spawning unit type of {unitType}";
        }
    }
}