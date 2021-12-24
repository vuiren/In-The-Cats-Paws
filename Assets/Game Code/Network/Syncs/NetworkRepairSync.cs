using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
    public interface INetworkRepairSync
    {
        void RepairPoint(RepairPoint repairPoint);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkRepairSync:MonoBehaviour, INetworkRepairSync
    {
        private PhotonView _photonView;
        private IRoomsService _roomsService;
        private IRepairPointsService _repairPointsService;

        [Inject]
        public void Construct(IRepairPointsService repairPointsService, IRoomsService roomsService)
        {
            _roomsService = roomsService;
            _repairPointsService = repairPointsService;
            _photonView = GetComponent<PhotonView>();
        }

        [PunRPC]
        private void RepairPointRPC(int roomId)
        {
            var room = _roomsService.GetRoomById(roomId);
            var repairPoint = _repairPointsService.GetRepaintPointInRoom(room);
            repairPoint.FixProgress();
        }
        
        public void RepairPoint(RepairPoint repairPoint)
        {
            var roomId = _roomsService.GetRoomId(repairPoint.room);
            _photonView.RPC("RepairPointRPC", RpcTarget.All, roomId);
        }
    }
}