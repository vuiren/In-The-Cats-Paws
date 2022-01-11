using System;
using Game_Project.Scripts.LogicLayer.Services;

namespace Game_Project.Scripts.LogicLayer.Interfaces
{
    public interface ITurnService
    {
        void OnTurn(Action<Turn> action);
        Turn CurrentTurn();
        void EndCurrentTurn();
        void AddEngineersSkippingTurn();
        bool IsEngineerSkippingTurn();
    }
}