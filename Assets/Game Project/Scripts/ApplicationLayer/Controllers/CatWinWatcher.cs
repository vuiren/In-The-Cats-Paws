using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class CatWinWatcher
    {
        private readonly IUnitExplosionService _explosionService;
        private readonly IUnitsService _unitsService;
        private readonly IWinService _winService;

        public CatWinWatcher(IWinService winService, IUnitsService unitsService, IUnitExplosionService explosionService)
        {
            _explosionService = explosionService;
            _unitsService = unitsService;
            _winService = winService;
            _explosionService.OnUnitExplosion(CheckIfCatWin);
        }

        private async void CheckIfCatWin(Unit obj)
        {
            var engineer = _unitsService.GetUnitsByUnitType(UnitType.Engineer).FirstOrDefault();

            if(engineer == null) return;

            if (engineer.Room == obj.Room)
            {
                await WaitDelay();
                _winService.CatWin();
            }
        }
        
        private async UniTask WaitDelay()
        {
            await Task.Delay(500);
        }
    }
}