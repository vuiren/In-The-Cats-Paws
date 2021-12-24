using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;
// ReSharper disable UnusedMember.Local

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
        private ILogger _logger;

        [Inject]
        public void Construct(ILogger logger, IUnitRoomService unitRoomService)
        {
            _logger = logger;
            _unitRoomService = unitRoomService;
        }
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        [Inject]
        public NetworkRoomsSync(ILogger logger, IUnitRoomService unitRoomService)
        {
            _logger = logger;
            _unitRoomService = unitRoomService;
        }

        [PunRPC]
        // ReSharper disable once UnusedMember.Local
        private void RegisterUnitToRoomRPC(string unitName, int roomId)
        {
            _logger.Log(this, $"Registering unit {unitName} to room with id {roomId}");
            _unitRoomService.TryToAddUnitToRoom(unitName, roomId);
            _logger.Log(this, $"Done registering unit {unitName} to room with id {roomId}");
        }

        [PunRPC]
        // ReSharper disable once InconsistentNaming
        private void RemoveUnitFromRoomPRC(string unitName, int roomId)
        {
            _logger.Log(this, $"Removing unit {unitName} from room with id {roomId}");
            _unitRoomService.RemoveUnitFromRoom(unitName, roomId);
            _logger.Log(this, $"Done removing unit {unitName} from room with id {roomId}");
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