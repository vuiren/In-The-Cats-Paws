using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public class PlayersService: IPlayersService
    {
        private readonly List<Player> _players = new();
        
        public void RegisterPlayer(PlayerType playerType)
        {
            _players.Add(new Player() {PlayerType = playerType, IsReady = false});
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