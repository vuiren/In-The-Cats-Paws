using System;
using Game_Project.Scripts.DataLayer;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IPlayersService
    {
        void RegisterPlayer(PlayerType playerType);
        void OnPlayerRegistered(Action<Player> action);
        Player[] GetAll();
        void PlayerReady(PlayerType playerType);
    }
}