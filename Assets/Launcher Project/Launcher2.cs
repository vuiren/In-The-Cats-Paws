using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Launcher_Project
{
    public class Launcher2 : MonoBehaviourPunCallbacks
    {
        [SerializeField] private string playerName, roomName;
        [SerializeField] private Image statusImage;
        [SerializeField] private Button joinRoomButton;
        [SerializeField] private GameObject joinRoomUI, joinArenaUI;
        
        private const string GameVersion = "1";

        
        private void Start()
        {
            joinArenaUI.SetActive(false);
            statusImage.color = Color.red;
            joinRoomButton.interactable = false;
            
            ConnectToPhoton();
        }
        
        private static void ConnectToPhoton()
        {
            if (PhotonNetwork.IsConnected) return;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnConnected()
        {
            base.OnConnected();
            
            statusImage.color = Color.green;
            joinRoomButton.interactable = true;
        }
        
        public void JoinRoom()
        {
            if (!PhotonNetwork.IsConnected) return;
            statusImage.color = Color.yellow;
        
            PhotonNetwork.LocalPlayer.NickName = playerName; //1
            Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " +
                      roomName);
            var roomOptions = new RoomOptions() { MaxPlayers = 2 }; //2
            var typedLobby = new TypedLobby(roomName, LobbyType.Default); //3
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby); //4
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Room joined");

            base.OnJoinedRoom();
            
                        
            joinRoomUI.SetActive(false);
            joinArenaUI.SetActive(true);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            
            if(!joinRoomUI) return;
            joinRoomUI.SetActive(true);
            joinArenaUI.SetActive(false);
        }

        public void SetRoomName(string roomName)
        {
            this.roomName = roomName;
        }

        public void SetPlayerName(string playerName)
        {
            this.playerName = playerName;
        }
    }
}