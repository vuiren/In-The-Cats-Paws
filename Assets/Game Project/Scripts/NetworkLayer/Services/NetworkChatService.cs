using System;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Photon.Pun;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkChatService: MonoBehaviourPun, IChatService
    {
        private Action<string> _onMessageReceived;
        
        public void SendChatMessage(string message)
        {
            photonView.RPC("ReceiveMessageRPC", RpcTarget.All, message);
        }

        [PunRPC]
        private void ReceiveMessageRPC(string message)
        {
            _onMessageReceived?.Invoke(message);
        }

        public void OnMessageReceived(Action<string> action)
        {
            _onMessageReceived += action;
        }
    }
}