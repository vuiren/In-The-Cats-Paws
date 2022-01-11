using System.Collections.Generic;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using DG.Tweening;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Drawers
{
    public sealed class ExplodingUnitAnimator
    {
        private IUnitExplosionService _explosionService;
        private Dictionary<Unit, Tween> _tweens = new();

        public ExplodingUnitAnimator(IUnitExplosionService explosionService)
        {
            explosionService.OnUnitExplosionStart(AnimateUnit);
            explosionService.OnUnitExplosion(StopAnimatingUnit);
        }

        private void StopAnimatingUnit(Unit obj)
        {
            _tweens[obj].Kill();
            _tweens.Remove(obj);
            var renderer = obj.GameObjectLink.GetComponentInChildren<SpriteRenderer>();
            renderer.DOColor(Color.white, 1f);
        }

        private void AnimateUnit(Unit obj)
        {
            var renderer = obj.GameObjectLink.GetComponentInChildren<SpriteRenderer>();
            var tween = renderer.DOColor(Color.red, 1f).SetLoops(-1, LoopType.Yoyo);
            _tweens.Add(obj, tween);
        }
    }
}