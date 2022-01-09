using System;
using Game_Project.Scripts.LogicLayer.Services;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface IGameStatusService
    {
        GameStatus GetGameStatus();
        void SetGameStatus(GameStatus gameStatus);
        void RegisterForGameStart(Action action);
    }
}