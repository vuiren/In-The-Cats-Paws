using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Code.MonoBehaviours
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] string roomName, playerName;
        [SerializeField] bool offlineMode;
        [SerializeField] GameObject roomJoinUI, buttonLoadArena, buttonJoinRoom;
        [SerializeField] Text playerStatusText, waitingText;
        [SerializeField] private byte maxPlayersPerRoom = 2;

        private const string gameVersion = "1";

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.OfflineMode = offlineMode;
        }

        private void Start()
        {
            //1
            PlayerPrefs.DeleteAll();

            Debug.Log("Connecting to Photon Network");

            //2
            roomJoinUI.SetActive(false);
            buttonLoadArena.SetActive(false);

            //3
            ConnectToPhoton();
        }

        private void ConnectToPhoton()
        {
            if (PhotonNetwork.IsConnected) return;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

        public void JoinRoom()
        {
            if (!PhotonNetwork.IsConnected) return;
        
            PhotonNetwork.LocalPlayer.NickName = playerName; //1
            Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " +
                      roomName);
            var roomOptions = new RoomOptions() { MaxPlayers = maxPlayersPerRoom }; //2
            var typedLobby = new TypedLobby(roomName, LobbyType.Default); //3
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby); //4
        }

        public void LoadArena()
        {
            // 5
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
            {
                if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.LoadLevel("Game Scene");
            }
            else
            {
                playerStatusText.text = "Minimum 2 Players required to Load Arena!";
            }
        }

        public override void OnConnected()
        {
            base.OnConnected();

            playerStatusText.text = "Connected to Photon!";
            playerStatusText.color = Color.green;
            roomJoinUI.SetActive(true);
            buttonLoadArena.SetActive(false);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
            {
                buttonLoadArena.SetActive(true);
                waitingText.gameObject.SetActive(false);
            }
            
            base.OnPlayerEnteredRoom(newPlayer);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            
            waitingText.gameObject.SetActive(true);

            if (PhotonNetwork.IsMasterClient)
            {
                buttonJoinRoom.SetActive(false);
                waitingText.text = "Waiting for second player";
                playerStatusText.text = "You are Lobby Leader";
            }
            else
            {
                playerStatusText.text = "Connected to Lobby";
                waitingText.text = "Waiting for master to start game";
            }

            roomJoinUI.SetActive(false);
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