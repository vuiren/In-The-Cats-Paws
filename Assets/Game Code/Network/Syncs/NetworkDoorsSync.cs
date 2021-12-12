using Game_Code.Services;
using Photon.Pun;
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
        private IDoorsService _doorsService;
        private PhotonView _photonView;
        
        [Inject]
        public void Construct(IDoorsService doorsService)
        {
            _doorsService = doorsService;
            _photonView = GetComponent<PhotonView>();
        }

        [PunRPC]
        // ReSharper disable once UnusedMember.Local
        private void SwitchDoorStateRPC(int doorId) => 
            _doorsService.SwitchDoorState(doorId);
        

        public void SwitchDoorState(int doorId) => 
            _photonView.RPC("SwitchDoorStateRPC", RpcTarget.All, doorId);
    }
}