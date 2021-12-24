using System;
using Game_Code.MonoBehaviours.Units.CatUnits;

namespace Game_Code.Controllers.CatBotControllers
{

    public class CatBotRevivalController
    {
        private CatBomb _catBomb;
        private int _turnsLeftUntilRevival;
        private readonly ILogger _logger;
        private Action<CatBomb> _onRevival;

        public CatBotRevivalController(ILogger logger, ICatBotExplosionController explosionController)
        {
            _logger = logger;
            explosionController.OnExplosion(x=>StartReviving(x as CatBomb, 1));
        }

        private void StartReviving(CatBomb catBomb, int turnsCount)
        {
            _logger.Log(this, $"Cat bomb {catBomb.name} started to revive");
            _catBomb = catBomb;
            _catBomb.SetState(CatBombState.Reviving);
            _turnsLeftUntilRevival = turnsCount;
            _logger.Log(this, $"Cat bomb {catBomb.name} will revive in {turnsCount} turns");
        }

        public void OnRevival(Action<CatBomb> action)
        {
            _onRevival += action;
        }

        private void Tick()
        {
            if(!_catBomb || _catBomb.GetCatBombState() != CatBombState.Reviving) return;

            _turnsLeftUntilRevival--;
            if (_turnsLeftUntilRevival <= 0)
            {
                _onRevival?.Invoke(_catBomb);
            }
        }
    }
}