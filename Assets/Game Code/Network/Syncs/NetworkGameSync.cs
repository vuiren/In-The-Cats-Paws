using Photon.Pun;
using UnityEngine;

namespace Game_Code.Network.Syncs
{
    public interface INetworkGameSync
    {
        void CatWin();
        void EngineerWin();
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkGameSync: MonoBehaviour, INetworkGameSync
    {
        private PhotonView _photonView;
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

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