using System.Linq;
using Game_Code.Controllers.CatBotControllers;
using Game_Code.MonoBehaviours.Units;
using Game_Code.MonoBehaviours.Units.CatUnits;
using Game_Code.Network.Syncs;

namespace Game_Code.Services
{
    public interface IExplosionService
    {
        void ExplodeCatBomb(CatBomb catBomb);
    }
    
    public class ExplosionService: IExplosionService
    {
        private ICatBotExplosionController _explosionController;
        private IUnitRoomService _unitRoomService;
        private INetworkGameSync _gameSync;


        public ExplosionService(ICatBotExplosionController explosionController, 
            IUnitRoomService unitRoomService, INetworkGameSync gameSync)
        {
            _explosionController = explosionController;
            _unitRoomService = unitRoomService;
            _gameSync = gameSync;
            
            _explosionController.OnExplosion(ExplodeCatBomb);
        }

        public void ExplodeCatBomb(CatBomb catBomb)
        {
            var room = _unitRoomService.FindUnitRoom(catBomb);
            var engineerInRoom = _unitRoomService.GetAllUnitsInRoom(room).SingleOrDefault(x=>x.UnitType() == UnitType.Engineer);
            if (engineerInRoom != null)
            {  
                _gameSync.CatWin();
            }
        }
    }
}