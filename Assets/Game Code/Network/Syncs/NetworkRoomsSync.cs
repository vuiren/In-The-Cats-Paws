using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
    public interface INetworkRoomsSync
    {
        void RegisterUnitToRoom(string unitName, int roomId);
        void RemoveUnitFromRoom(string unitName, int roomId);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkRoomsSync:MonoBehaviour, INetworkRoomsSync
    {
        private IUnitRoomService _unitRoomService;
        private PhotonView _photonView;

        [Inject]
        public void Construct(IUnitRoomService unitRoomService)
        {
            _unitRoomService = unitRoomService;
        }
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        [Inject]
        public NetworkRoomsSync(IUnitRoomService unitRoomService)
        {
            _unitRoomService = unitRoomService;
        }

        [PunRPC]
        private void RegisterUnitToRoomRPC(string unitName, int roomId)
        {
            _unitRoomService.TryToAddUnitToRoom(unitName, roomId);
        }

        [PunRPC]
        private void RemoveUnitFromRoomPRC(string unitName, int roomId)
        {
            _unitRoomService.RemoveUnitFromRoom(unitName, roomId);
        }

        public void RegisterUnitToRoom(string unitName, int roomId)
        {
            _photonView.RPC("RegisterUnitToRoomRPC", RpcTarget.All, unitName, roomId);
        }

        public void RemoveUnitFromRoom(string unitName, int roomId)
        {
            _photonView.RPC("RemoveUnitFromRoomPRC", RpcTarget.All, unitName, roomId);
        }
    }
}