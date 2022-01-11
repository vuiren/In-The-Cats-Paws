using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Photon.Pun;
using UnityEngine;
using Zenject;
using Room = Game_Project.Scripts.DataLayer.Level.Room;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkRoomsService : MonoBehaviourPun, IRoomsService
    {
        private IMyLogger _logger;
        private readonly Dictionary<Vector2Int, Room> _map = new();
        private bool pieceOfShitOfCode = false;

        [Inject]
        public void Construct()
        {
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
            photonView.RPC("RegisterRoomRPC", RpcTarget.All,
                room.GameObjectLink.name);
        }

        public Vector3 GetPlaceInRoom(Vector2Int room)
        {
            _logger.Log($"Getting place from room: {room}");
            var freePoints = new Queue<Vector3>(_map[room].FreePoints);
            var freePoint = freePoints.Dequeue();
            _map[room].FreePoints = freePoints.ToList();
            return freePoint;
        }

        public void ReturnPlaceInRoom(Vector2Int room, Vector3 point)
        {
            photonView.RPC("ReturnPlaceInRoomRPC", RpcTarget.All, room.x, room.y, point);
        }

        [PunRPC]
        private void GetPlaceInRoomRPC(int x, int y, PhotonMessageInfo info)
        {
            var room = new Vector2Int(x, y);
            _logger.Log($"Getting place in room {room} over network");

            var player = PhotonNetwork.LocalPlayer;
            if (info.Sender.UserId == player.UserId)
            {
                _logger.Log("Prevented double call");
                return;
            }

            _logger.Log(info.ToString());
            var freePoints = new Queue<Vector3>(_map[room].FreePoints);
            freePoints.Dequeue();
            _map[room].FreePoints = freePoints.ToList();
        }

        [PunRPC]
        private void ReturnPlaceInRoomRPC(int roomX, int roomY, Vector3 point)
        {
            var room = new Vector2Int(roomX, roomY);
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