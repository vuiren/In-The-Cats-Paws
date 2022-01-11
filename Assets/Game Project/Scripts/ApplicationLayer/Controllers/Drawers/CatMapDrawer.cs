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
        private readonly IRoomsService _roomsService;
        private readonly IUnitsSelectionService _selectionService;
        private readonly ITurnService _turnService;

        public CatMapDrawer(ITurnService turnService, ICorridorsService corridorsService,
            IUnitsService unitsService, IRepairPointsService repairPointsService, IButtonsService buttonsService,
            IUnitsSelectionService selectionService, ICurrentPlayerService currentPlayerService,
            IRoomsService roomsService)
        {
            _turnService = turnService;
            _unitsService = unitsService;
            _corridorsService = corridorsService;
            _buttonsService = buttonsService;
            _repairPointsService = repairPointsService;
            _selectionService = selectionService;
            _roomsService = roomsService;

            if (currentPlayerService.CurrentPlayerType() != PlayerType.SmartCat) return;

            turnService.OnTurn(ReDrawMap);
            _selectionService.RegisterOnUnitSelection(x => ReDrawMap());
            _selectionService.RegisterOnUnitDeselection(x => ReDrawMap());
        }

        public void ReDrawMap()
        {
            DrawCorridors();
            DrawUnits();
            DrawRooms();

            foreach (var repairPoint in _repairPointsService.GetAll())
            {
                var view = repairPoint.GameObjectLink.GetComponent<RepairPointView>();
                view.Draw(false);
            }

            var smartCatUnitTypes = new[] {UnitType.CatBotBiter, UnitType.CatBotBomb, UnitType.CatBotButtonPusher};

            var smartCatBots = _unitsService
                .GetAll()
                .Where(x => smartCatUnitTypes.Contains(x.UnitType));

            var catBots = smartCatBots as Unit[] ?? smartCatBots.ToArray();
            DrawForButtonPusher(catBots);
            DrawForBiter(catBots);
            DrawForBomb(catBots);
        }

        private void DrawRooms()
        {
            foreach (var room in _roomsService.GetAll())
            {
                var view = room.GameObjectLink.GetComponent<RoomView>();

                if (room.ShadowRoom)
                {
                    var draw = _unitsService
                        .GetAll()
                        .Where(x=>x.UnitType != UnitType.Engineer)
                        .Any(x => x.Room == room.Coords);

                    view.Draw(draw);
                }
                else
                {
                    view.Draw(true);
                }
            }
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
            if (_turnService.IsEngineerSkippingTurn())
            {
                var biter2 = smartCatBots.SingleOrDefault(x => x.UnitType == UnitType.CatBotBiter);
                if (biter2 == null) return;
                
                var biter2View = biter2.GameObjectLink.GetComponent<CatBiteView>();
                biter2View.Draw(false);
                
                return;
            }
            
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
            var units = _unitsService.GetAll();
            foreach (var unit in units)
            {
                var view = unit.GameObjectLink.GetComponent<UnitView>();

                if (unit.UnitType == UnitType.Engineer)
                {
                    var anyUnitInTheSameRoomWithEngineer =
                        units.Any(x => x.Room == unit.Room && x.UnitType != UnitType.Engineer);

                    if (anyUnitInTheSameRoomWithEngineer)
                    {
                        view.DrawUnit(true);
                        continue;
                    }
                    
                    var hideEngineer = _roomsService
                        .GetAll()
                        .Where(x => x.ShadowRoom)
                        .Any(x => x.Coords == unit.Room);
                    
                    view.DrawUnit(!hideEngineer);
                }
                else
                {
                    view.DrawUnit(true);
                }
            }
        }

        private void DrawCorridors()
        {
            foreach (var corridor in _corridorsService.GetAll())
            {
                var view = corridor.GameObjectLink.GetComponent<CorridorView>();

                if (corridor.ShadowCorridor)
                {
                    var draw = _unitsService
                        .GetAll()
                        .Where(x=>x.UnitType != UnitType.Engineer)
                        .Any(x => x.Room == corridor.Room1 || x.Room == corridor.Room2);
                    
                    view.DrawCorridor(draw);
                }
                else
                {
                    view.DrawCorridor(true);
                }
            }
        }

        private void ReDrawMap(Turn obj)
        {
            ReDrawMap();
        }
    }
}