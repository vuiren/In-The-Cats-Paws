using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class EngineerWinWatcher
    {
        private readonly IRepairPointsService _repairPointsService;
        private readonly IWinService _winService;
        
        public EngineerWinWatcher(ICurrentPlayerService currentPlayerService,
            ITurnService turnService, IRepairPointsService repairPointsService, IWinService winService)
        {
            if(currentPlayerService.CurrentPlayerType() != PlayerType.Engineer) return;
            _repairPointsService = repairPointsService;
            _winService = winService;
            turnService.OnTurn(CheckIfEngineerWin);
        }

        private async void CheckIfEngineerWin(Turn turn)
        {
            var repairPoints = _repairPointsService.GetAll();

            if (repairPoints.All(x => x.PointFixed))
            {
                await WaitDelay();
                _winService.EngineerWin();
            }
        }

        private async UniTask WaitDelay()
        {
            await Task.Delay(10);
        }
    }
}