using System;
using Game_Project.Scripts.ViewLayer.Entities.Units.CatUnits.CatBomb;

namespace Game_Project.Scripts.ViewLayer.Controllers.CatBotControllers
{
    public interface ICatBotExplosionController
    {
        void StartExploding(CatBombView catBombView, int turnsCount);
        int TurnsUntilExplosion();
        void OnExplosion(Action<CatBombView> action);
        void OnExplodingStart(Action<CatBombView> action);
    }
    
    public class CatBotExplosionController: ICatBotExplosionController
    {
        /*private readonly IMyLogger _logger;
        private CatBomb _catBomb;
        private int _turnsLeft;
        private Action<CatBomb> _onExplosionStart, _onExplosion;
        
        public CatBotExplosionController(IMyLogger logger, ITurnService turnService)
        {
            _logger = logger;
            turnService.OnSmartCatTurn(Tick);
        }
        
        public void StartExploding(CatBomb catBomb, int turnsCount)
        {
            _logger.Log( $"{catBomb.name} is starting to explode");
            _catBomb = catBomb;
            _catBomb.SetState(CatBombState.Exploding);
            _turnsLeft = turnsCount;
            _onExplosionStart?.Invoke(catBomb);
            _logger.Log( $"{_catBomb.name} will explode in {turnsCount} turns");
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
            _logger.Log( $"{_turnsLeft} turns left until {_catBomb.name} will explode");
            _turnsLeft--;

            if (_turnsLeft <= 0)
            {
                Explode();
            }
        }

        private void Explode()
        {
            _logger.Log( $"Cat bomb {_catBomb.name} exploded");
            _onExplosion?.Invoke(_catBomb);
        }*/
        public void StartExploding(CatBombView catBombView, int turnsCount)
        {
            throw new NotImplementedException();
        }

        public int TurnsUntilExplosion()
        {
            throw new NotImplementedException();
        }

        public void OnExplosion(Action<CatBombView> action)
        {
            throw new NotImplementedException();
        }

        public void OnExplodingStart(Action<CatBombView> action)
        {
            throw new NotImplementedException();
        }
    }
}