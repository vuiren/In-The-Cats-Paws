using Game_Code.Services;
using Photon.Pun;
using QFSW.QC;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
    public interface INetworkDoorsSync
    {
        void SwitchDoorState(int doorId);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkDoorsSync: MonoBehaviour, INetworkDoorsSync
    {
        private ILogger _logger;
        private IDoorsService _doorsService;
        private PhotonView _photonView;
        
        [Inject]
        public void Construct(ILogger logger, IDoorsService doorsService)
        {
            _logger = logger;
            _doorsService = doorsService;
            _photonView = GetComponent<PhotonView>();
        }

        [PunRPC]
        // ReSharper disable once UnusedMember.Local
        private void SwitchDoorStateRPC(int doorId) => 
            _doorsService.SwitchDoorState(doorId);
        

        [Command("network.switchdoorstate")]
        public void SwitchDoorState(int doorId)
        {
            _logger.Log(this, $"Switching state of door with id {doorId}");
            _photonView.RPC("SwitchDoorStateRPC", RpcTarget.All, doorId);
            _logger.Log(this, $"Done switching state of door with id {doorId}");
        }
    }
}