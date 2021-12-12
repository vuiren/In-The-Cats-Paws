using System;

namespace Game_Code.Services
{
    public enum GameStatus {Preparing, Going}
    public interface IGameStatusService
    {
        GameStatus GetGameStatus();
        void SetGameStatus(GameStatus gameStatus);
        void RegisterForGameStart(Action action);
    }
    
    public class GameStatusService: IGameStatusService
    {
        private GameStatus _gameStatus = GameStatus.Preparing;
        private readonly ILogger _logger;

        public GameStatusService(ILogger logger)
        {
            _logger = logger;
            SetGameStatus(GameStatus.Preparing);
        }
        private Action OnGameStarts { get; set; }
        
        
        public GameStatus GetGameStatus()
        {
            return _gameStatus;
        }

        public void SetGameStatus(GameStatus gameStatus)
        {
            _logger.Log($"Setting game status to {gameStatus}");
            
            _gameStatus = gameStatus;

            switch (_gameStatus)
            {
                case GameStatus.Going:
                    OnGameStarts?.Invoke();
                    break;
                case GameStatus.Preparing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RegisterForGameStart(Action action)
        {
            OnGameStarts += action;
        }
    }
}