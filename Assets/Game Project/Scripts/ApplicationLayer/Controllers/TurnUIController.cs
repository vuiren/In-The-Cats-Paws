using System;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class TurnUIController
    {
        private ITurnService turnService;
        private readonly PlayerType _playerType;
        private readonly GameObject smartCatTurnUI;
        private readonly GameObject engineerTurnUI;

        public TurnUIController(ICurrentPlayerService currentPlayerService, ITurnService turnService, SceneData sceneData)
        {
            _playerType = currentPlayerService.CurrentPlayerType();
            smartCatTurnUI = sceneData.catTurnUI;
            engineerTurnUI = sceneData.engineerTurnUI;

            turnService.OnTurn(RedrawUI);
        }

        public void RedrawUI(Turn obj)
        {
            if (obj.ToString() == _playerType.ToString())
            {
                smartCatTurnUI.SetActive(false);
                engineerTurnUI.SetActive(false);
                return;
            }

            switch (obj)
            {
                case Turn.Engineer:
                    smartCatTurnUI.SetActive(false);
                    engineerTurnUI.SetActive(true);
                    break;
                case Turn.SmartCat:
                    smartCatTurnUI.SetActive(true);
                    engineerTurnUI.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}