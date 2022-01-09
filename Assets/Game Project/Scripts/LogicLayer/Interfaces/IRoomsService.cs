using System.Collections.Generic;
using Game_Project.Scripts.DataLayer.Level;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IRoomsService
    {
        IEnumerable<Room> GetAll();
        Room GetRoomByCoord(Vector2Int coord);
        void RegisterRoom(Room room);
        Vector3 GetPlaceInRoom(Vector2Int room);
        void ReturnPlaceInRoom(Vector2Int room, Vector3 point);
    }
}