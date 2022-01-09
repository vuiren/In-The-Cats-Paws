using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.NetworkLayer.Syncs
{
    public interface INetworkFreeRoomPointsSync
    {
        Vector3 GetFreePointFromRoom(int roomId);
        void AddFreePointToRoom(int roomId, Vector3 point);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public sealed class NetworkFreeRoomPointsSync: MonoBehaviour, INetworkFreeRoomPointsSync
    {
        private IMyLogger _myLogger;
        private PhotonView _photonView;
        private IRoomsPointsService _roomsPointsService;
        private IRoomsService _roomsService;

        [Inject]
        public void Construct(IMyLogger myLogger, IRoomsPointsService roomsPointsService, IRoomsService roomsService)
        {
            _myLogger = myLogger;
            _roomsPointsService = roomsPointsService;
            _roomsService = roomsService;
            _photonView = GetComponent<PhotonView>();
        }

        [PunRPC]
        private void GetFreePointFromRoomRPC(int roomId)
        {
            /*var room = _roomsService.GetRoomById(roomId);
            _roomsPointsService.GetFreePointFromRoom(room);*/
        }

        [PunRPC]
        private void AddFreePointToRoomRPC(int roomId, Vector3 point)
        {
            /*var room = _roomsService.GetRoomById(roomId);
            _roomsPointsService.AddFreePointToRoom(room, point);*/
        }

        public Vector3 GetFreePointFromRoom(int roomId)
        {
            /*_myLogger.Log( $"Getting free point from room with id {roomId}");
            
            _photonView.RPC("GetFreePointFromRoomRPC", RpcTarget.Others, roomId);
            var room = _roomsService.GetRoomById(roomId);
            var point = _roomsPointsService.GetFreePointFromRoom(room); 
            
            _myLogger.Log( $"Done getting free point from room {room.gameObject.name} {point}");
            
            return point;*/
            return Vector3.zero;;
        }

        public void AddFreePointToRoom(int roomId, Vector3 point)
        {
            _myLogger.Log( $"Adding free point {point} to room with id {roomId}");
            
            _photonView.RPC("AddFreePointToRoomRPC", RpcTarget.All, roomId, point);
            
            _myLogger.Log( $"Done adding free point {point} to room with id {roomId}");
        }
    }
}