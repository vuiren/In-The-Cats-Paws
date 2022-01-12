using System;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.UI
{
    public sealed class ChatPanelUI: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private GameObject messagePrefab;
        [SerializeField] private string inputMessage;

        [Inject] private IChatService _chatService;

        private void Awake()
        {
            _chatService.OnMessageReceived(AddText);
        }

        private void AddText(string obj)
        {
            textMesh.text += obj + '\n';
        }

        public void SetInputMessage(string message)
        {
            inputMessage = message;
        }

        public void SendMessage()
        {
            var message = PhotonNetwork.LocalPlayer.NickName;
            _chatService.SendChatMessage(message+": " + inputMessage);
        }
        
    }
}