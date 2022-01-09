using System;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class TurnService: ITurnService
    {
        private Action<Turn> _onTick;
        private Turn _currentTurn;
        
        public void OnTurn(Action<Turn> action)
        {
            _onTick += action;
        }

        Turn ITurnService.CurrentTurn()
        {
            return _currentTurn;
        }

        public void EndCurrentTurn()
        {
            _currentTurn = _currentTurn switch
            {
                Turn.Engineer => Turn.SmartCat,
                Turn.SmartCat => Turn.Engineer,
                _ => throw new ArgumentOutOfRangeException()
            };

            _onTick?.Invoke(_currentTurn);
        }
    }
}