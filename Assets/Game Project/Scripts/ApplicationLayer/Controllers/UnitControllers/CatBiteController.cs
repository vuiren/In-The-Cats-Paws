using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBite;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.UnitControllers
{
    public sealed class CatBiteController
    {
        private ITurnService _turnService;
        private IMyLogger _logger;
        public CatBiteController(IUnitsService unitsService, ITurnService turnService)
        {
            _turnService = turnService;
            _logger = LoggerFactory.Create(this);
            unitsService.RegisterOnUnitRegistration(RegisterUnit);
        }

        private void RegisterUnit(Unit obj)
        {
            if (obj.UnitType == UnitType.CatBotBiter)
            {
                _logger.Log("Registering cat bite");

                var view = obj.GameObjectLink.GetComponent<CatBiteView>();
                view.OnBiteButtonPressed += BiteEngineer;
                _logger.Log("Done registering cat bite");
            }
        }

        public void BiteEngineer()
        {
            _logger.Log($"Someone is biting engineer!");
            _turnService.AddEngineersSkippingTurn();
            _turnService.EndCurrentTurn();
        }
    }
}