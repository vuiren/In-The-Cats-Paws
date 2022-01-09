namespace Game_Project.Scripts.ViewLayer.Controllers.CatBotControllers
{

    public class CatBotRevivalController
    {
        /*private CatBomb _catBomb;
        private int _turnsLeftUntilRevival;
        private readonly IMyLogger _logger;
        private Action<CatBomb> _onRevival;

        public CatBotRevivalController(IMyLogger logger, ICatBotExplosionController explosionController)
        {
            _logger = logger;
            explosionController.OnExplosion(x=>StartReviving(x as CatBomb, 1));
        }

        private void StartReviving(CatBomb catBomb, int turnsCount)
        {
            _logger.Log( $"Cat bomb {catBomb.name} started to revive");
            _catBomb = catBomb;
            _catBomb.SetState(CatBombState.Reviving);
            _turnsLeftUntilRevival = turnsCount;
            _logger.Log( $"Cat bomb {catBomb.name} will revive in {turnsCount} turns");
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
        }*/
    }
}