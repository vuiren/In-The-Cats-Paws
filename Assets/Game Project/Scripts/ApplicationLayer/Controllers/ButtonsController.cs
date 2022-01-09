using System.Linq;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class ButtonsController
    {
        private readonly ITurnService _turnService;
        private readonly ICorridorsService _corridorsService;

        public ButtonsController(ITurnService turnService, ICorridorsService corridorsService)
        {
            _corridorsService = corridorsService;
            _turnService = turnService;

            var buttons = Object.FindObjectsOfType<Entity<Button>>();

            foreach (var button in buttons)
            {
                var view = button.gameObject.GetComponent<ButtonView>();
                view.OnButtonPressed += CloseCorridor;
            }
        }

        private void CloseCorridor(ButtonView obj)
        {
            var corridor = _corridorsService.GetCorridor(obj.model.Room1, obj.model.Room2);
            _corridorsService.LockCorridor(obj.model.Room1, obj.model.Room2, !corridor.Locked);
            _turnService.EndCurrentTurn();
        }
    }
}