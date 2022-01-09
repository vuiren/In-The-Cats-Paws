using System;
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
    public sealed class CorridorsService: ICorridorsService
    {
        private readonly IMyLogger _logger;
        private Dictionary<Tuple<Vector2Int, Vector2Int>, Corridor> _corridors = new();

        [Inject]
        public CorridorsService()
        {
            _logger = LoggerFactory.Create(this);
        }
        
        public IEnumerable<Corridor> GetAll()
        {
            return _corridors.Values.ToArray();
        }

        public void RegisterCorridor(Corridor corridor)
        {
            var key = new Tuple<Vector2Int, Vector2Int>(corridor.Room1, corridor.Room2);

            if (_corridors.ContainsKey(key))
            {
                _logger.LogWarning($"Corridor between rooms {key.Item1} and {key.Item2} already exists");
                return;
            }
            _corridors.Add(key, corridor);
        }

        public void LockCorridor(Vector2Int room1, Vector2Int room2, bool doLock)
        {
            var key = new Tuple<Vector2Int, Vector2Int>(room1, room2);

            if (!_corridors.ContainsKey(key))
                key = new Tuple<Vector2Int, Vector2Int>(room2, room1);
                
            if (_corridors.ContainsKey(key))
            {
                _corridors[key].Locked = doLock;
            }
            else
            {
                _logger.LogWarning($"Corridor between rooms {key.Item1} and {key.Item2} not found");
            }
        }

        public Corridor GetCorridor(Vector2Int room1, Vector2Int room2)
        {
            var key = new Tuple<Vector2Int, Vector2Int>(room1, room2);

            if (!_corridors.ContainsKey(key))
                key = new Tuple<Vector2Int, Vector2Int>(room2, room1);
                
            if (_corridors.ContainsKey(key))
            {
                return _corridors[key];
            }
            else
            {
                _logger.LogWarning($"Corridor between rooms {key.Item1} and {key.Item2} not found");
                return null;
            }
        }
    }
}