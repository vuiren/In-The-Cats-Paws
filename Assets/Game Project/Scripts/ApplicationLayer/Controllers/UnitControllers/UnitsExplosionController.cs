using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.UnitControllers
{
    public sealed class UnitsExplosionController
    {
        private readonly IMyLogger _logger;
        private ITurnService _turnService;
        private readonly IUnitsService _unitsService;
        private readonly IUnitExplosionService _explosionService;

        public UnitsExplosionController(ITurnService turnService, IUnitsService unitsService,
            IUnitExplosionService explosionService)
        {
            _logger = LoggerFactory.Create(this);
            _unitsService = unitsService;
            _explosionService = explosionService;
            turnService.OnTurn(TickExplosionService);
        }

        public void TickExplosionService()
        {
            _explosionService.TickTimer();
        }
        
        private void TickExplosionService(Turn obj)
        {
            if(obj == Turn.SmartCat)
            _explosionService.TickTimer();
        }

        public void ExplodeUnit(int unitId, int turnsUntilExplosion)
        {
            var catBomb = _unitsService.GetUnitByID(unitId);

            if (catBomb == null)
            {
                _logger.LogWarning($"Unit with {unitId} not found");
                return;
            }

            if (catBomb.UnitType != UnitType.CatBotBomb)
            {
                _logger.LogWarning("Unit type is not a cat bomb");
                return;
            }
            
            _explosionService.RegisterUnitForExplosion(catBomb, turnsUntilExplosion);
        }
    }
}