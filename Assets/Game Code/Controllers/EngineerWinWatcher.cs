using System.Linq;
using Game_Code.Managers;
using Game_Code.Network.Syncs;
using Game_Code.Services;

namespace Game_Code.Controllers
{
    public class EngineerWinWatcher
    {
        private readonly IRepairPointsService _repairPointsService;
        private readonly INetworkGameSync _gameSync;

        public EngineerWinWatcher(CurrentPlayerManager currentPlayerManager,
            ITurnService turnService, IRepairPointsService repairPointsService, INetworkGameSync gameSync)
        {
            if(currentPlayerManager.CurrentPlayerType != PlayerType.Engineer) return;
            _gameSync = gameSync;
            _repairPointsService = repairPointsService;
            turnService.OnEngineerTurn(CheckIfEngineerWin);
            turnService.OnSmartCatTurn(CheckIfEngineerWin);
        }

        private void CheckIfEngineerWin()
        {
            var repairPoints = _repairPointsService.GetAll();

            if (repairPoints.All(x => x.pointFixed))
            {
                _gameSync.EngineerWin();
            }
        }
    }
}