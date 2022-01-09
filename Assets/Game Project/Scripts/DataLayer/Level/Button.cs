using System;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Level
{
    [Serializable]
    public sealed class Button : EntityModel
    {
        [SerializeField] private Vector2Int buttonRoom;

        [SerializeField] private Vector2Int room1, room2;

        public Vector2Int ButtonRoom
        {
            get => buttonRoom;
            set => buttonRoom = value;
        }

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

        public override string ToString()
        {
            return $"[Button] Button in the room {buttonRoom}. Closing corridor between {room1} amd {room2}";
        }
    }
}