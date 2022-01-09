using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class RepairPointsController
    {
        private readonly ITurnService _turnService;
        private readonly IRepairPointsService _repairPointsService;

        public RepairPointsController(ITurnService turnService, IRepairPointsService repairPointsService)
        {
            _turnService = turnService;
            _repairPointsService = repairPointsService;
            
            var repairPoints = Object.FindObjectsOfType<Entity<RepairPoint>>();

            foreach (var repairPoint in repairPoints)
            {
                var view = repairPoint.gameObject.GetComponent<RepairPointView>();
                view.OnRepairPointFixButtonPressed += ReactToButton;
            }
        }

        private void ReactToButton(RepairPointView obj)
        {
            _repairPointsService.FixRepairPoint(obj.model);
            var repairPoint = _repairPointsService.GetRepaintPointInRoom(obj.model.Room);
            obj.model = repairPoint;
            _turnService.EndCurrentTurn();
        }
    }
}