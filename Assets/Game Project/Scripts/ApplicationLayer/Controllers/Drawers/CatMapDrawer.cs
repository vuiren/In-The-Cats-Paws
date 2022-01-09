using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBite;
using Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBomb;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Drawers
{
    public sealed class CatMapDrawer : IMapDrawer
    {
        private readonly IUnitsService _unitsService;
        private readonly IButtonsService _buttonsService;
        private readonly ICorridorsService _corridorsService;
        private readonly IRepairPointsService _repairPointsService;
        private readonly IUnitsSelectionService _selectionService;

        public CatMapDrawer(ITurnService turnService, ICorridorsService corridorsService,
            IUnitsService unitsService, IRepairPointsService repairPointsService, IButtonsService buttonsService,
            IUnitsSelectionService selectionService, ICurrentPlayerService currentPlayerService)
        {
            _unitsService = unitsService;
            _corridorsService = corridorsService;
            _buttonsService = buttonsService;
            _repairPointsService = repairPointsService;
            _selectionService = selectionService;

            if (currentPlayerService.CurrentPlayerType() != PlayerType.SmartCat) return;
            
            turnService.OnTurn(ReDrawMap);
            _selectionService.RegisterOnUnitSelection(x=>ReDrawMap());
            _selectionService.RegisterOnUnitDeselection(x=>ReDrawMap());
        }

        public void ReDrawMap()
        {
            DrawCorridors();
            DrawUnits();

            foreach (var repairPoint in _repairPointsService.GetAll())
            {
                var view = repairPoint.GameObjectLink.GetComponent<RepairPointView>();
                view.Draw(false);
            }

            var smartCatUnitTypes = new[] {UnitType.CatBotBiter, UnitType.CatBotBomb, UnitType.CatBotButtonPusher};

            var smartCatBots = _unitsService
                .GetAll()
                .Where(x => smartCatUnitTypes.Contains(x.UnitType));

            DrawForButtonPusher(smartCatBots);
            DrawForBiter(smartCatBots);
            DrawForBomb(smartCatBots);
        }

        private void DrawForBomb(IEnumerable<Unit> smartCatBots)
        {
            var bomb = smartCatBots.SingleOrDefault(x => x.UnitType == UnitType.CatBotBomb);
            if (bomb == null) return;

            var selectedUnit = _selectionService.GetSelectedUnit();
            if (selectedUnit == null)
            {
                bomb.GameObjectLink.GetComponent<CatBombView>().Draw(false);
                return;
            }

            bomb.GameObjectLink.GetComponent<CatBombView>().Draw(selectedUnit.UnitType == UnitType.CatBotBomb);
        }

        private void DrawForBiter(IEnumerable<Unit> smartCatBots)
        {
            var engineer = _unitsService.GetUnitsByUnitType(UnitType.Engineer).FirstOrDefault();
            if (engineer == null) return;
            var engineerRoom = engineer.Room;

            var biter = smartCatBots.SingleOrDefault(x => x.UnitType == UnitType.CatBotBiter);
            if (biter == null) return;
            var biterRoom = biter.Room;

            var biterView = biter.GameObjectLink.GetComponent<CatBiteView>();
            biterView.Draw(engineerRoom == biterRoom);
        }

        private void DrawForButtonPusher(IEnumerable<Unit> smartCatBots)
        {
            var buttonViews = new List<ButtonView>();

            foreach (var button in _buttonsService.GetAll())
            {
                var buttonView = button.GameObjectLink.GetComponent<ButtonView>();
                buttonViews.Add(buttonView);
                buttonView.DrawButton(false);
            }

            var buttonPusher = smartCatBots.SingleOrDefault(x => x.UnitType == UnitType.CatBotButtonPusher);
            if (buttonPusher == null) return;
            var buttonPusherRoom = buttonPusher.Room;

            var buttonsToDraw = buttonViews
                .Where(x => x.model.ButtonRoom == buttonPusherRoom);

            foreach (var buttonView in buttonsToDraw)
            {
                buttonView.DrawButton(true);
            }
        }

        private void DrawUnits()
        {
            foreach (var unit in _unitsService.GetAll())
            {
                var view = unit.GameObjectLink.GetComponent<UnitView>();
                view.DrawUnit(true);
            }
        }

        private void DrawCorridors()
        {
            foreach (var corridor in _corridorsService.GetAll())
            {
                var view = corridor.GameObjectLink.GetComponent<CorridorView>();
                view.DrawCorridor(true);
            }
        }

        private void ReDrawMap(Turn obj)
        {
            ReDrawMap();
        }
    }
}