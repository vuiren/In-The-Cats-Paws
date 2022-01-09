using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBomb;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class CatBombController
    {
        private IUnitExplosionService _explosionService;

        public CatBombController(IUnitExplosionService explosionService, IUnitsService unitsService)
        {
            unitsService.RegisterOnUnitRegistration(TryToRegister);
            _explosionService = explosionService;
        }

        private void TryToRegister(Unit obj)
        {
            if(obj.UnitType != UnitType.CatBotBomb) return;

            var view = obj.GameObjectLink.GetComponent<CatBombView>();
            view.OnExplodeButtonPressed += StartExploding;
        }

        private void StartExploding(Unit arg1, int arg2)
        {
            _explosionService.RegisterUnitForExplosion(arg1, arg2);
        }
    }
}