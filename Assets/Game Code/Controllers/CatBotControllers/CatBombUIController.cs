using Game_Code.MonoBehaviours.Units;
using Game_Code.MonoBehaviours.Units.CatUnits;
using Game_Code.Services;

namespace Game_Code.Controllers.CatBotControllers
{
    public class CatBombUIController
    {
        private readonly ILogger _logger;
        
        public CatBombUIController(ILogger logger, IUnitsSelectionService selectionService)
        {
            _logger = logger;
            selectionService.RegisterOnUnitSelection(TryToShowUI);
            selectionService.RegisterOnUnitDeselection(TryToHideUI);
        }

        private void TryToHideUI(IUnit obj)
        {
            if (obj.UnitType() == UnitType.CatBotBomb)
            {
                HideCatBombUI(obj as CatBomb);
            }
        }

        private void TryToShowUI(IUnit obj)
        {
            if (obj.UnitType() == UnitType.CatBotBomb)
            {
                ShowCatBombUI(obj as CatBomb);
            }
        }

        private void ShowCatBombUI(CatBomb catBomb)
        {
            _logger.Log(this, $"Showing {catBomb.name} ui");
            catBomb.ShowUI();
            _logger.Log(this, $"Done showing {catBomb.name} ui");
        }

        private void HideCatBombUI(CatBomb catBomb)
        {
            _logger.Log(this, $"Hiding {catBomb.name} ui");
            catBomb.HideUI();
            _logger.Log(this, $"Done hiding {catBomb.name} ui");
        }
    }
}