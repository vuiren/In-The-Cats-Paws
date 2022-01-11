using Game_Project.Scripts.LogicLayer.Interfaces;
using Photon.Pun;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkWinService : MonoBehaviourPun, IWinService
    {
        public void CatWin()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("CatWinScene");
            }
        }

        public void EngineerWin()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("EngineerWinScene");
            }
        }
    }
}