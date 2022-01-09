using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Level
{
    [Serializable]
    public sealed class RepairPoint : EntityModel
    {
        [SerializeField] [AllowNesting] [ReadOnly]
        private bool pointFixed;

        [SerializeField] [AllowNesting] [ReadOnly]
        private int turnsToFix;

        [SerializeField] [AllowNesting] [ReadOnly]
        private Vector2Int room;

        [SerializeField] [AllowNesting] [ReadOnly]
        private int turnsLeftToFix;

        public bool PointFixed
        {
            get => pointFixed;
            set => pointFixed = value;
        }

        public int TurnsToFix
        {
            get => turnsToFix;
            set => turnsToFix = value;
        }

        public int TurnsLeftToFix
        {
            get => turnsLeftToFix;
            set => turnsLeftToFix = value;
        }

        public Vector2Int Room
        {
            get => room;
            set => room = value;
        }

        public override string ToString()
        {
            return $"Repair point in room {room}. Turns left to fix: {turnsLeftToFix}. IsFixed: {pointFixed}";
        }
    }
}