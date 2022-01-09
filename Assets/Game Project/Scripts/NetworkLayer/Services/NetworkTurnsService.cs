using System;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.NetworkLayer.Base;
using Photon.Pun;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public class NetworkTurnsService:NetworkService, ITurnService
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
            PhotonView.RPC("EndCurrentTurnRPC", RpcTarget.All);
        }

        [PunRPC]
        private void EndCurrentTurnRPC()
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