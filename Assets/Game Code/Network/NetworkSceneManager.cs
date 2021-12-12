using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Game_Code.Network
{
    public class NetworkSceneManager:MonoBehaviourPunCallbacks
    {
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            SceneManager.LoadScene(0);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
			
            SceneManager.LoadScene(0);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}