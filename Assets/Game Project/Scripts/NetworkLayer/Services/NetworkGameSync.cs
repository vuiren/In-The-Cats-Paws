using Photon.Pun;
using UnityEngine;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public interface INetworkGameSync
    {
        void CatWin();
        void EngineerWin();
    }
    
    public class NetworkGameSync: MonoBehaviourPun, INetworkGameSync
    {
        private PhotonView _photonView;

        [PunRPC]
        private void CatWinRPC()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("CatWinScene");
            }
        }
        
        [PunRPC]
        private void EngineerWinRPC()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("EngineerWinScene");
            }
        }

        public void CatWin()
        {
            _photonView.RPC("CatWinRPC", RpcTarget.All);
        }

        public void EngineerWin()
        {
            _photonView.RPC("EngineerWinRPC", RpcTarget.All);
        }
    }
}