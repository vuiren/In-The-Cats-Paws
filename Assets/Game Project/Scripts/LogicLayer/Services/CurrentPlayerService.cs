using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public class CurrentPlayerService:ICurrentPlayerService
    {
        public PlayerType CurrentPlayerType()
        {
            return PlayerType.SmartCat;
        }
    }
}