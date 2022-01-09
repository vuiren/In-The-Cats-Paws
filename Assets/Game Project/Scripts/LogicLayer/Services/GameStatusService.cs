using System;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public class GameStatusService: IGameStatusService
    {
        private GameStatus _gameStatus = GameStatus.Preparing;
        private readonly IMyLogger _logger;

        public GameStatusService()
        {
            _logger = LoggerFactory.Create(this);
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
            _logger.Log($"Done setting game status to {gameStatus}");
        }

        public void RegisterForGameStart(Action action)
        {
            OnGameStarts += action;
        }
    }
}