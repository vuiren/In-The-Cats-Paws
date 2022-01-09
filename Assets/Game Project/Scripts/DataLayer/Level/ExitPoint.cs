using System;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Level
{
    [Serializable]
    public class ExitPoint:EntityModel
    {
        [SerializeField] private Vector2Int room;

        public Vector2Int Room
        {
            get => room;
            set => room = value;
        }

        public override string ToString()
        {
            return $"Exit point in room {room}";
        }
    }
}