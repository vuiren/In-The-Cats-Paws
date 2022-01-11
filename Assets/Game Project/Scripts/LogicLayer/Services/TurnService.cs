using System;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.LogicLayer.Services
{
    public sealed class TurnService: ITurnService
    {
        private Action<Turn> _onTick;
        private Turn _currentTurn;
        private int _turnsEngineerSkipping;
        
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

            if (_currentTurn == Turn.Engineer)
            {
                _turnsEngineerSkipping--;
            }

            _turnsEngineerSkipping = _turnsEngineerSkipping <= 0 ? 0 : _turnsEngineerSkipping;
            _onTick?.Invoke(_currentTurn);
        }

        public void AddEngineersSkippingTurn()
        {
            _turnsEngineerSkipping ++;
        }

        public bool IsEngineerSkippingTurn()
        {
            return _turnsEngineerSkipping > 0;
        }
    }
}