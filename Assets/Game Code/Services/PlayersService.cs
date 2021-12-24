using System;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Players;

namespace Game_Code.Services
{
    public interface IPlayersService
    {
        Player[] GetAll();
        void RegisterPlayer(Player player);
        PlayerEngineer GetPlayerEngineer();
        PlayerSmartCat GetPlayerSmartCat();
        void OnPlayerAdded(Action<Player> action);
    }
    
    public class PlayersService: IPlayersService
    {
        private PlayerEngineer _playerEngineer;
        private PlayerSmartCat _playerSmartCat;
        private int _initializedPlayersCount;
        private readonly int _startPlayersCount;
        private Action<Player> _onPlayerAdded;
        private readonly IGameStatusService _gameStatusService;
        private readonly ILogger _logger;

        public PlayersService(ILogger logger, StaticData staticData, IGameStatusService gameStatusService)
        {
            _logger = logger;
            _startPlayersCount = staticData.playersCount;
            _gameStatusService = gameStatusService;
        }

        public Player[] GetAll()
        {
            return new[] {_playerEngineer as Player, _playerSmartCat as Player};
        }

        public void RegisterPlayer(Player player)
        {
            _logger.Log(this,$"Registering player {player.name}");   
            if(!_playerEngineer && player is PlayerEngineer engineer)
            {
                _playerEngineer = engineer;
                _initializedPlayersCount++;
            }

            if (!_playerSmartCat && player is PlayerSmartCat smartCat)
            {
                _playerSmartCat = smartCat;
                _initializedPlayersCount++;
            }
            
            _logger.Log(this,$"Player {player.name} has been registered");
            _onPlayerAdded?.Invoke(player);

            if(_initializedPlayersCount >= _startPlayersCount)
            {
                _gameStatusService.SetGameStatus(GameStatus.Going);
            }
        }

        public PlayerEngineer GetPlayerEngineer() => _playerEngineer;

        public PlayerSmartCat GetPlayerSmartCat() => _playerSmartCat;

        public void OnPlayerAdded(Action<Player> action)
        {
            _onPlayerAdded += action;
        }
    }
}