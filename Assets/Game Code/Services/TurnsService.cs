using System;

namespace Game_Code.Services
{
    public enum Turn
    {
        Engineer,
        SmartCat,
    }
    
    public interface ITurnService
    {
        void OnEngineerTurn(Action action);
        void OnSmartCatTurn(Action action);
        Turn CurrentTurn();
        void EndCurrentTurn();
    }
    
    public class TurnService: ITurnService
    {
        private Action _onEngineerTurnStart;
        private Action _onSmartCatTurnStart;
        private Turn _currentTurn;
        
        public void OnEngineerTurn(Action action)
        {
            _onEngineerTurnStart += action;
        }

        public void OnSmartCatTurn(Action action)
        {
            _onSmartCatTurnStart += action;
        }

        Turn ITurnService.CurrentTurn()
        {
            return _currentTurn;
        }

        public void EndCurrentTurn()
        {
            switch (_currentTurn)
            {
                case Turn.Engineer:
                    _currentTurn = Turn.SmartCat;
                    _onSmartCatTurnStart?.Invoke();
                    break;
                case Turn.SmartCat:
                    _currentTurn = Turn.Engineer;
                    _onEngineerTurnStart?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}