using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Mono.CSharp;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public class NetworkCorridorsService : MonoBehaviourPun, ICorridorsService
    {
        private IMyLogger _logger;
        private readonly Dictionary<Tuple<Vector2Int, Vector2Int>, Corridor> _corridors = new();

        [Inject]
        public void Construct()
        {
            _logger = LoggerFactory.Create(this);
        }

        public IEnumerable<Corridor> GetAll()
        {
            return _corridors.Values.ToArray();
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

        public void RegisterCorridor(Corridor corridor)
        {
            photonView.RPC("RegisterCorridorRPC", RpcTarget.All,
                 corridor.GameObjectLink.name);
        }

        public void LockCorridor(Vector2Int room1, Vector2Int room2, bool doLock)
        {
            photonView.RPC("LockCorridorRPC", RpcTarget.All, 
                 room1.x, room1.y, room2.x, room2.y, doLock);
        }
        
        [PunRPC]
        private void RegisterCorridorRPC(string linkName)
        {
            var link = GameObject.Find(linkName);
            var corridor = link.GetComponent<CorridorView>().model;

            var key = new Tuple<Vector2Int, Vector2Int>(corridor.Room1, corridor.Room2);

            if (_corridors.ContainsKey(key))
            {
                _logger.LogWarning($"Corridor between rooms {key.Item1} and {key.Item2} already exists");
                return;
            }

            _corridors.Add(key, corridor);
        }

        [PunRPC]
        private void LockCorridorRPC(int room1X, int room1Y, int room2X, int room2Y, bool doLock)
        {
            var room1 = new Vector2Int(room1X, room1Y);
            var room2 = new Vector2Int(room2X, room2Y);
            
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
    }
}