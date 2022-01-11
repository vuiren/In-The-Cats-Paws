using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Drawers
{
    public sealed class EngineerMapDrawer : IMapDrawer
    {
        private readonly IUnitsService _unitsService;
        private readonly IButtonsService _buttonsService;
        private readonly ICorridorsService _corridorsService;
        private readonly IRepairPointsService _repairPointsService;
        private readonly IRoomsService _roomsService;

        public EngineerMapDrawer(ITurnService turnService, ICorridorsService corridorsService,
            IUnitsService unitsService, IRepairPointsService repairPointsService, IButtonsService buttonsService,
            ICurrentPlayerService currentPlayerService, IRoomsService roomsService)
        {
            _unitsService = unitsService;
            _corridorsService = corridorsService;
            _buttonsService = buttonsService;
            _repairPointsService = repairPointsService;
            _roomsService = roomsService;
            if (currentPlayerService.CurrentPlayerType() == PlayerType.Engineer)
                turnService.OnTurn(ReDrawMap);
        }

        public void ReDrawMap()
        {
            var engineer = _unitsService.GetUnitsByUnitType(UnitType.Engineer).FirstOrDefault();
            if (engineer == null) return;
            var engineerRoom = engineer.Room;

            DrawCorridors(engineerRoom);
            DrawUnits(engineerRoom);
            DrawButtons(engineerRoom);
            DrawRepairPoints(engineerRoom);
            DrawRooms(engineerRoom);
        }

        private void DrawRooms(Vector2Int engineerRoom)
        {
            var roomViews = new List<RoomView>();
            foreach (var room in _roomsService.GetAll())
            {
                var view = room.GameObjectLink.GetComponent<RoomView>();
                roomViews.Add(view);
                view.Draw(false);
            }

            var roomsToDraw = roomViews
                .Where(x => x.model.Coords == engineerRoom || x.model.ShadowRoom);
            
            foreach (var view in roomsToDraw)
            {
                if (view.model.ShadowRoom)
                {
                    if (view.model.Coords == engineerRoom)
                    {
                        view.Draw(true);
                    }
                    else
                    {
                        view.DrawForEngineer();
                    }
                    continue;
                }
                view.Draw(true);
            }
        }

        private void DrawRepairPoints(Vector2Int engineerRoom)
        {
            var repairPointViews = new List<RepairPointView>();

            foreach (var repairPoint in _repairPointsService.GetAll())
            {
                var view = repairPoint.GameObjectLink.GetComponent<RepairPointView>();
                repairPointViews.Add(view);
                view.Draw(false);
            }

            var repairPointsToDraw = repairPointViews
                .Where(x => x.model.Room == engineerRoom && !x.model.PointFixed);
            
            foreach (var repairPointView in repairPointsToDraw)
            {
                repairPointView.Draw(true);
            }
        }

        private void DrawButtons(Vector2Int engineerRoom)
        {
            var buttonViews = new List<ButtonView>();

            foreach (var button in _buttonsService.GetAll())
            {
                var buttonView = button.GameObjectLink.GetComponent<ButtonView>();
                buttonViews.Add(buttonView);
                buttonView.DrawButton(false);
            }

            var buttonsToDraw = buttonViews
                .Where(x => x.model.ButtonRoom == engineerRoom);

            foreach (var buttonView in buttonsToDraw)
            {
                buttonView.DrawButton(true);
            }
        }

        private void DrawUnits(Vector2Int engineerRoom)
        {
            var unitViews = new List<UnitView>();
            var units = _unitsService.GetAll();

            foreach (var unit in units)
            {
                var view = unit.GameObjectLink.GetComponent<UnitView>();
                unitViews.Add(view);
                view.DrawUnit(false);
            }

            var unitsToDraw = unitViews
                .Where(x => x.model.Room == engineerRoom)
                .ToArray();

            foreach (var unit in unitsToDraw)
            {
                unit.DrawUnit(true);
            }
        }

        private void DrawCorridors(Vector2Int engineerRoom)
        {
            var corridorViews = new List<CorridorView>();

            foreach (var corridor in _corridorsService.GetAll())
            {
                var view = corridor.GameObjectLink.GetComponent<CorridorView>();
                corridorViews.Add(view);
                view.DrawCorridor(false);
            }

            var corridorsToDraw = corridorViews
                .Where(x => x.model.Room1 == engineerRoom || x.model.Room2 == engineerRoom);

            foreach (var corridor in corridorsToDraw)
            {
                corridor.DrawCorridor(true);
            }
        }

        private void ReDrawMap(Turn obj)
        {
            ReDrawMap();
        }
    }
}