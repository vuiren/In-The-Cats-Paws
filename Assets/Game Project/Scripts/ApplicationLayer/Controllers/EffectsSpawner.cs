using System.Linq;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Data;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class EffectsSpawner
    {
        private readonly Effect _explosionEffect;

        private readonly IUnitsService _unitsService;
        private IUnitExplosionService _explosionService;

        public EffectsSpawner(IUnitExplosionService explosionService, IUnitsService unitsService, StaticData staticData)
        {
            _unitsService = unitsService;
            _explosionService = explosionService;
            
            _explosionEffect = staticData.effects.Single(x => x.effectType == EffectType.ExplosionEffect);
            explosionService.OnUnitExplosion(CreateExplosionEffect);
        }

        public void CreateExplosionEffect(int unitId)
        {
            var unit = _unitsService.GetUnitByID(unitId);
            CreateExplosionEffect(unit);
        }

        public void CreateExplosionEffect(Unit obj)
        {
            var effectInstance =
                Object.Instantiate(_explosionEffect.effectPrefab, obj.Position, new Quaternion());

            Object.Destroy(effectInstance, _explosionEffect.effectLiveTime);
        }
    }
}