using DG.Tweening;
using Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBomb;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Controllers.CatBotControllers
{
    public class CatBotExplodingAnimator
    {
        private ICatBotExplosionController _explosionController;
        private Tween _tween;

        public CatBotExplodingAnimator(ICatBotExplosionController explosionController)
        {
            explosionController.OnExplodingStart(StartAnimation);
            explosionController.OnExplosion(StopAnimation);
        }

        private void StartAnimation(CatBombView obj)
        {
       //     _tween = obj.GetComponentInChildren<SpriteRenderer>().(Color.red, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private void StopAnimation(CatBombView obj)
        {
            _tween.Kill();
          //  obj.GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 1f);
        }
    }
}