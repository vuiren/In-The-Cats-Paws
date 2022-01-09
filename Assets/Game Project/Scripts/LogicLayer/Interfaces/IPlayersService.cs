using Game_Project.Scripts.DataLayer;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IPlayersService
    {
        void RegisterPlayer(PlayerType playerType);
        Player[] GetAll();
        void PlayerReady(PlayerType playerType);
    }
}