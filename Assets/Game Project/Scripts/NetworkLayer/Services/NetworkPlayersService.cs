using System;
using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Photon.Pun;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkPlayersService: MonoBehaviourPun, IPlayersService
    {
        private readonly List<Player> _players = new();
        private Action<Player> _onPlayerRegistered;

        public void RegisterPlayer(PlayerType playerType)
        {
            photonView.RPC("RegisterPlayerRPC", RpcTarget.All, playerType);
        }

        public void OnPlayerRegistered(Action<Player> action)
        {
            _onPlayerRegistered += action;
        }

        public Player[] GetAll()
        {
            return _players.ToArray();
        }

        public void PlayerReady(PlayerType playerType)
        {
            photonView.RPC("PlayerReadyRPC", RpcTarget.All, playerType);
        }


        [PunRPC]
        private void RegisterPlayerRPC(PlayerType playerType)
        {
            var player = new Player() {PlayerType = playerType, IsReady = false};
            _players.Add(player);
            _onPlayerRegistered?.Invoke(player);
        }
        
        [PunRPC]
        private void PlayerReadyRPC(PlayerType playerType)
        {
            _players.Single(x => x.PlayerType == playerType).IsReady = true;
        }
    }
}