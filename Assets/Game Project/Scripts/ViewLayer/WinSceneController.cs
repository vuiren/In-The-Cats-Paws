using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Project.Scripts.ViewLayer
{
    public class WinSceneController : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                button.interactable = PhotonNetwork.IsMasterClient;
                button.GetComponentInChildren<Text>().text = "Waiting for master client";
            }
        }

        public void RetryButton()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Network Game Scene");
            }
        }
    }
}
