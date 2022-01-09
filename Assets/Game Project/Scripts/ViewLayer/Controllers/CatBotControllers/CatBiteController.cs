using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBite;

namespace Game_Project.Scripts.ViewLayer.Controllers.CatBotControllers
{
    public interface ICatBiteController
    {
        void Bite();
        void ShowUI(CatBiteView catBiteView);
        void HideUI(CatBiteView catBiteView);
    }
    
    internal sealed class CatBiteController: ICatBiteController
    {
        private readonly IUnitsService _unitsService;

        public CatBiteController(IUnitsService unitsService,ITurnService turnService)
        {
            _unitsService = unitsService;
            
            turnService.OnTurn(RefreshCatBiteBotsUI);
        }

        private void RefreshCatBiteBotsUI(Turn obj)
        {
            var catBiteBotsModels = _unitsService.GetUnitsByUnitType(UnitType.CatBotBiter);
            foreach (var unit in catBiteBotsModels)
            {
                var catBiteBot = unit.GameObjectLink.GetComponent<CatBiteView>();
                HideUI(catBiteBot);
                ShowUI(catBiteBot);
            }        }

        public void Bite()
        {
            
        }

        public void ShowUI(CatBiteView catBiteView)
        {
            /*var catBiteRoom = _unitRoomService.FindUnitRoom(catBite.GetUnitModel());
            
            if(catBiteRoom == null) return;

            var engineer = _unitsService.GetUnitsByUnitType(UnitType.Engineer);
            var units = engineer.ToArray();
            if(!units.Any()) return;
            
            var engineerRoom = _unitRoomService.FindUnitRoom(units.First());
            if(engineerRoom == null) return;

            if (catBiteRoom == engineerRoom)
            {
                catBite.ShowUI();
            }*/
        }

        public void HideUI(CatBiteView catBiteView)
        {
            catBiteView.HideUI();
        }
    }
}