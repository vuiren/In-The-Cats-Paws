using System;
using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class PlayersService: IPlayersService
    {
        private readonly List<Player> _players = new();
        private Action<Player> _onPlayerRegistered;
        
        public void RegisterPlayer(PlayerType playerType)
        {
            var player = new Player() {PlayerType = playerType, IsReady = false};
            _players.Add(player);
            _onPlayerRegistered?.Invoke(player);
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
            _players.Single(x => x.PlayerType == playerType).IsReady = true;
        }
    }
}