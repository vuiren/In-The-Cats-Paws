using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.NetworkLayer.Base;
using Photon.Pun;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkPlayersService:NetworkService, IPlayersService
    {
        private readonly List<Player> _players = new();


        public void RegisterPlayer(PlayerType playerType)
        {
            PhotonView.RPC("RegisterPlayerRPC", RpcTarget.All, playerType);
        }

        public Player[] GetAll()
        {
            return _players.ToArray();
        }

        public void PlayerReady(PlayerType playerType)
        {
            PhotonView.RPC("PlayerReadyRPC", RpcTarget.All, playerType);
        }


        [PunRPC]
        private void RegisterPlayerRPC(PlayerType playerType)
        {
            _players.Add(new Player() {PlayerType = playerType, IsReady = false});
        }
        
        [PunRPC]
        private void PlayerReadyRPC(PlayerType playerType)
        {
            _players.Single(x => x.PlayerType == playerType).IsReady = true;
        }
    }
}