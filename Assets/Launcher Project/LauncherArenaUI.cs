﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Launcher_Project
{
    public class LauncherArenaUI : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text player1Text, player2Text;
        [SerializeField] private Button startGameButton;
        [SerializeField] private GameObject levelSelectionUI;
        public override void OnEnable()
        {
            SetUI();

            base.OnEnable();
        }

        private void SetUI()
        {
            startGameButton.interactable = false;
            player1Text.text = "";
            player2Text.text = "";

            startGameButton.GetComponentInChildren<Text>().text = !PhotonNetwork.IsMasterClient ? 
                "Ждём создателя комнаты" : "Ждём второго игрока";

            levelSelectionUI.SetActive(PhotonNetwork.IsMasterClient);
        }


        private void RefreshUI()
        {
            var index = 0;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (index == 0)
                {
                    player1Text.text = player.NickName;
                }

                if (index == 1)
                {
                    player2Text.text = player.NickName;
                }

                index++;
            }
            
            levelSelectionUI.SetActive(PhotonNetwork.IsMasterClient);

        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            RefreshUI();
        }


        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            RefreshUI();

            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                startGameButton.GetComponentInChildren<Text>().text = "Начать игру";
                levelSelectionUI.SetActive(PhotonNetwork.IsMasterClient);
                startGameButton.interactable = true;
            }
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            player1Text.text = "";
            player2Text.text = "";
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            player2Text.text = "";
            startGameButton.GetComponentInChildren<Text>().text = "Ждём второго игрока";
            startGameButton.interactable = false;
        }

        private int _sceneIndex = 1;
        
        public void LoadArena()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel("Network Game Scene " + _sceneIndex);
        }

        public void SetSceneIndex(int index)
        {
            _sceneIndex = index + 1;
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}