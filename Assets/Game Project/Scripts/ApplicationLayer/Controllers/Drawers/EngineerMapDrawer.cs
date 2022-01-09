using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Drawers
{
    public sealed class EngineerMapDrawer : IMapDrawer
    {
        private readonly IUnitsService _unitsService;
        private readonly IButtonsService _buttonsService;
        private readonly ICorridorsService _corridorsService;
        private readonly IRepairPointsService _repairPointsService;

        public EngineerMapDrawer(ITurnService turnService, ICorridorsService corridorsService,
            IUnitsService unitsService, IRepairPointsService repairPointsService, IButtonsService buttonsService,
            ICurrentPlayerService currentPlayerService)
        {
            _unitsService = unitsService;
            _corridorsService = corridorsService;
            _buttonsService = buttonsService;
            _repairPointsService = repairPointsService;

            if (currentPlayerService.CurrentPlayerType() == PlayerType.Engineer)
                turnService.OnTurn(ReDrawMap);
        }

        public void ReDrawMap()
        {
            var corridorViews = new List<CorridorView>();

            foreach (var corridor in _corridorsService.GetAll())
            {
                var view = corridor.GameObjectLink.GetComponent<CorridorView>();
                corridorViews.Add(view);
                view.DrawCorridor(false);
            }

            var unitViews = new List<UnitView>();

            foreach (var unit in _unitsService.GetAll())
            {
                var view = unit.GameObjectLink.GetComponent<UnitView>();
                unitViews.Add(view);
                view.DrawUnit(false);
            }

            var buttonViews = new List<ButtonView>();

            foreach (var button in _buttonsService.GetAll())
            {
                var buttonView = button.GameObjectLink.GetComponent<ButtonView>();
                buttonViews.Add(buttonView);
                buttonView.DrawButton(false);
            }

            var repairPointViews = new List<RepairPointView>();

            foreach (var repairPoint in _repairPointsService.GetAll())
            {
                var view = repairPoint.GameObjectLink.GetComponent<RepairPointView>();
                repairPointViews.Add(view);
                view.Draw(false);
            }

            var engineer = _unitsService.GetUnitsByUnitType(UnitType.Engineer).FirstOrDefault();
            if (engineer == null) return;

            var engineerRoom = engineer.Room;

            var corridorsToDraw = corridorViews
                .Where(x => x.model.Room1 == engineerRoom || x.model.Room2 == engineerRoom)
                .ToArray();

            var unitsToDraw = unitViews
                .Where(x => _unitsService.GetUnitByID(x.model.ID).Room == engineerRoom)
                .ToArray();

            var buttonsToDraw = buttonViews
                .Where(x => x.model.ButtonRoom == engineerRoom)
                .ToArray();

            var repairPointsToDraw = repairPointViews
                .Where(x => x.model.Room == engineerRoom && !x.model.PointFixed)
                .ToArray();

            foreach (var corridor in corridorsToDraw)
            {
                corridor.DrawCorridor(true);
            }

            foreach (var unit in unitsToDraw)
            {
                unit.DrawUnit(true);
            }

            foreach (var buttonView in buttonsToDraw)
            {
                buttonView.DrawButton(true);
            }

            foreach (var repairPointView in repairPointsToDraw)
            {
                repairPointView.Draw(true);
            }
        }

        private void ReDrawMap(Turn obj)
        {
            ReDrawMap();
        }
    }
}