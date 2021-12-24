using System;
using Game_Code.MonoBehaviours.Units.CatUnits;
using Game_Code.Services;

namespace Game_Code.Controllers.CatBotControllers
{
    public interface ICatBotExplosionController
    {
        void StartExploding(CatBomb catBomb, int turnsCount);
        int TurnsUntilExplosion();
        void OnExplosion(Action<CatBomb> action);
        void OnExplodingStart(Action<CatBomb> action);
    }
    
    public class CatBotExplosionController: ICatBotExplosionController
    {
        private readonly ILogger _logger;
        private CatBomb _catBomb;
        private int _turnsLeft;
        private Action<CatBomb> _onExplosionStart, _onExplosion;
        
        public CatBotExplosionController(ILogger logger, ITurnService turnService)
        {
            _logger = logger;
            turnService.OnSmartCatTurn(Tick);
        }
        
        public void StartExploding(CatBomb catBomb, int turnsCount)
        {
            _logger.Log(this, $"{catBomb.name} is starting to explode");
            _catBomb = catBomb;
            _catBomb.SetState(CatBombState.Exploding);
            _turnsLeft = turnsCount;
            _onExplosionStart?.Invoke(catBomb);
            _logger.Log(this, $"{_catBomb.name} will explode in {turnsCount} turns");
        }

        public int TurnsUntilExplosion() => _turnsLeft;
        public void OnExplosion(Action<CatBomb> action)
        {
            _onExplosion += action;
        }

        public void OnExplodingStart(Action<CatBomb> action)
        {
            _onExplosionStart += action;
        }

        private void Tick()
        {
            if (!_catBomb || _catBomb.GetCatBombState() != CatBombState.Exploding) return;
            _logger.Log(this, $"{_turnsLeft} turns left until {_catBomb.name} will explode");
            _turnsLeft--;

            if (_turnsLeft <= 0)
            {
                Explode();
            }
        }

        private void Explode()
        {
            _logger.Log(this, $"Cat bomb {_catBomb.name} exploded");
            _onExplosion?.Invoke(_catBomb);
        }
    }
}