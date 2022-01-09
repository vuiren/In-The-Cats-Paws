using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Level
{
    [Serializable]
    public sealed class Corridor: EntityModel
    {
        [SerializeField] [AllowNesting] [ReadOnly] private Vector2Int room1, room2;
        [SerializeField] private bool locked;

        public Vector2Int Room1
        {
            get => room1;
            set => room1 = value;
        }

        public Vector2Int Room2
        {
            get => room2;
            set => room2 = value;
        }

        public bool Locked
        {
            get => locked;
            set => locked = value;
        }

        public override string ToString()
        {
            return $"[Corridor] {base.ToString()} Corridor between {room1} and {room2}. Is locked: {locked}";
        }
    }
}