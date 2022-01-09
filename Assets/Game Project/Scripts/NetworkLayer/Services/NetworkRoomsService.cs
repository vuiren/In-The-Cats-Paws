using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.NetworkLayer.Base;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Photon.Pun;
using UnityEngine;
using Zenject;
using Room = Game_Project.Scripts.DataLayer.Level.Room;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkRoomsService : NetworkService, IRoomsService
    {
        private IMyLogger _logger;
        private readonly Dictionary<Vector2Int, Room> _map = new();

        [Inject]
        protected override void Construct()
        {
            base.Construct();
            _logger = LoggerFactory.Create(this);
        }

        public IEnumerable<Room> GetAll() => _map.Values.ToArray();

        public Room GetRoomByCoord(Vector2Int coord)
        {
            if (_map.ContainsKey(coord))
            {
                return _map[coord];
            }

            _logger.LogWarning($"Room at {coord} has not been found");
            return null;
        }

        public void RegisterRoom(Room room)
        {
            PhotonView.RPC("RegisterRoomRPC", RpcTarget.All,
                room.GameObjectLink.name);
        }

        public Vector3 GetPlaceInRoom(Vector2Int room)
        {
            var freePoints = new Queue<Vector3>(_map[room].FreePoints);

            var freePoint = freePoints.Dequeue();

            _map[room].FreePoints = freePoints.ToList();
            PhotonView.RPC("GetPlaceInRoomRPC", RpcTarget.Others, room.x, room.y);
            return freePoint;
        }

        public void ReturnPlaceInRoom(Vector2Int room, Vector3 point)
        {
            PhotonView.RPC("ReturnPlaceInRoomRPC", RpcTarget.All, room.x, room.y, point);
        }

        [PunRPC]
        private void GetPlaceInRoomRPC(int x, int y)
        {
            var room = new Vector2Int(x, y);
            var freePoints = new Queue<Vector3>(_map[room].FreePoints);
            freePoints.Dequeue();
            _map[room].FreePoints = freePoints.ToList();
        }

        
        [PunRPC]
        private void ReturnPlaceInRoomRPC(int roomX, int roomY, Vector3 point)
        {
            var room = new Vector2Int(roomX, roomY);
            if (_map.ContainsKey(room))
                _map[room].FreePoints.Add(point);
        }

        [PunRPC]
        // ReSharper disable once UnusedMember.Local
        private void RegisterRoomRPC(string linkName)
        {
            var link = GameObject.Find(linkName);
            var roomModel = link.GetComponent<RoomView>().model;

            var coords = roomModel.Coords;

            _logger.Log($"Registering room {coords}");

            if (_map.ContainsKey(roomModel.Coords))
            {
                _logger.LogWarning($"Room with id {roomModel.ID} already registered");
            }
            else
            {
                _map.Add(roomModel.Coords, roomModel);
            }

            _logger.Log($"Done registering room {coords}");
        }
    }
}