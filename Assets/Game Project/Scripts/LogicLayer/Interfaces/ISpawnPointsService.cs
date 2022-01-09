using Game_Project.Scripts.DataLayer.Level;
using UnityEngine;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface ISpawnPointsService
    {
        SpawnPoint[] GetAll();
        void RegisterSpawnPoint(SpawnPoint spawnPoint);
        SpawnPoint GetSpawnPointInRoom(Vector2Int room);
    }
}