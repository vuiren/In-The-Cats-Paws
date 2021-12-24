using DG.Tweening;
using Game_Code.MonoBehaviours.Units.CatUnits;
using UnityEngine;

namespace Game_Code.Controllers.CatBotControllers
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

        private void StartAnimation(CatBomb obj)
        {
            _tween = obj.GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private void StopAnimation(CatBomb obj)
        {
            _tween.Kill();
            obj.GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 1f);
        }
    }
}