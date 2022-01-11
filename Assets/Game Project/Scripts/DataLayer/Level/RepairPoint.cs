using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Level
{
    public sealed class RepairPoint : EntityModel
    {
        private bool pointFixed;

        private int turnsToFix;

        private Vector2Int room;

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