using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public class SpawnPointsService: ISpawnPointsService
    {
        private Dictionary<Vector2Int, SpawnPoint> _spawnPoints = new();
        private IMyLogger _logger;

        [Inject]
        public SpawnPointsService()
        {
            _logger = LoggerFactory.Create(this);
        }
        
        public SpawnPoint[] GetAll()
        {
            return _spawnPoints.Values.ToArray();
        }

        public void RegisterSpawnPoint(SpawnPoint spawnPoint)
        {
            if (_spawnPoints.ContainsKey(spawnPoint.Room))
            {
                _logger.LogWarning($"{spawnPoint} already registered");
                return;
            }

            _spawnPoints.Add(spawnPoint.Room, spawnPoint);
        }

        public SpawnPoint GetSpawnPointInRoom(Vector2Int room)
        {
            if (_spawnPoints.ContainsKey(room))
            {
                return _spawnPoints[room];
            }
            
            _logger.LogWarning($"Spawn point at room {room} has not been found");
            return null;
        }
    }
}