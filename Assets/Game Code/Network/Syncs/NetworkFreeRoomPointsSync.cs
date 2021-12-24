using Game_Code.MonoBehaviours.Level;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
    public interface INetworkFreeRoomPointsSync
    {
        Vector3 GetFreePointFromRoom(Room room);
        void AddFreePointToRoom(Room room, Vector3 point);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkFreeRoomPointsSync: MonoBehaviour, INetworkFreeRoomPointsSync
    {
        private ILogger _logger;
        private PhotonView _photonView;
        private IRoomsPointsService _roomsPointsService;
        private IRoomsService _roomsService;
        
        [Inject]
        public void Construct(ILogger logger, IRoomsPointsService roomsPointsService, IRoomsService roomsService)
        {
            _logger = logger;
            _roomsPointsService = roomsPointsService;
            _roomsService = roomsService;
            _photonView = GetComponent<PhotonView>();
        }

        [PunRPC]
        private void GetFreePointFromRoomRPC(int roomId)
        {
            var room = _roomsService.GetRoomById(roomId);
            _roomsPointsService.GetFreePointFromRoom(room);
        }

        [PunRPC]
        private void AddFreePointToRoomRPC(int roomId, Vector3 point)
        {
            var room = _roomsService.GetRoomById(roomId);
            _roomsPointsService.AddFreePointToRoom(room, point);
        }

        public Vector3 GetFreePointFromRoom(Room room)
        {
            _logger.Log(this, $"Getting free point from room {room.gameObject.name}");
            var roomId = _roomsService.GetRoomId(room);
            _photonView.RPC("GetFreePointFromRoomRPC", RpcTarget.Others, roomId);
            var point = _roomsPointsService.GetFreePointFromRoom(room); 
            _logger.Log(this, $"Done getting free point from room {room.gameObject.name} {point}");
            return point;
        }

        public void AddFreePointToRoom(Room room, Vector3 point)
        {
            _logger.Log(this, $"Adding free point {point} to room {room.gameObject.name}");
            var roomId = _roomsService.GetRoomId(room);
            _photonView.RPC("AddFreePointToRoomRPC", RpcTarget.All, roomId, point);
            _logger.Log(this, $"Done adding free point {point} to room {room.gameObject.name}");
        }
    }
}