using System;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Photon.Pun;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public class NetworkTurnsService:MonoBehaviourPun, ITurnService
    {
        private Action<Turn> _onTick;
        private Turn _currentTurn;
        private int _turnsEngineerWillSkip;
        
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
            photonView.RPC("EndCurrentTurnRPC", RpcTarget.All);
        }

        public void AddEngineersSkippingTurn()
        {
            photonView.RPC("AddEngineersSkippingTurnRPC", RpcTarget.All);
        }

        [PunRPC]
        public void AddEngineersSkippingTurnRPC()
        {
            _turnsEngineerWillSkip += 2;
        }

        public bool IsEngineerSkippingTurn()
        {
            return _turnsEngineerWillSkip > 0;
        }

        [PunRPC]
        private void EndCurrentTurnRPC()
        {
            if (IsEngineerSkippingTurn())
            {
                _turnsEngineerWillSkip--;
                _onTick?.Invoke(_currentTurn);
                return;
            }
            
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