using System;
using System.Collections.Generic;
using System.Linq;
using Game_Code.MonoBehaviours.Units;
using Game_Code.Services;
using UnityEngine;

namespace Game_Code.Managers
{
    public class PlayerEngineerVisibilityManager
    {
        private readonly ILogger _logger;
        private readonly IUnitsService _unitsService;
        private readonly IUnitRoomService _unitRoomService;

        public PlayerEngineerVisibilityManager(ILogger logger, CurrentPlayerManager currentPlayerManager,
            IGameStatusService gameStatusService, IUnitsService unitsService, ITurnService turnService, 
            IUnitRoomService unitRoomService)
        {
            if (currentPlayerManager.CurrentPlayerType != PlayerType.Engineer) return;
            _logger = logger;
            turnService.OnSmartCatTurn(RefreshVisibility);
            turnService.OnEngineerTurn(RefreshVisibility);
            gameStatusService.RegisterForGameStart(RefreshVisibility);
            _unitsService = unitsService;
            _unitRoomService = unitRoomService;
        }

        private void RefreshVisibility()
        {
            try
            {
                _logger.Log("Refreshing visibility for player engineer");

                var smartCatBotsTypes = new[] {UnitType.CatBotBiter, UnitType.CatBotBomb, UnitType.CatBotButtonPusher};
            
                var engineerUnit = _unitsService.GetUnitsByUnitType(UnitType.Engineer).First();

                var catUnits = new List<IUnit>();
                foreach (var catBotType in smartCatBotsTypes)
                {
                    catUnits.AddRange(_unitsService.GetUnitsByUnitType(catBotType));
                }

                var engineerRoom = _unitRoomService.FindUnitRoom(engineerUnit);

                var catUnitsInDifferentRoom = catUnits.Where(x =>
                {
                    var unitRoom = _unitRoomService.FindUnitRoom(x);
                    return unitRoom != engineerRoom;
                });
                var catUnitsInSameRoom = catUnits.Where(x =>
                {
                    var unitRoom = _unitRoomService.FindUnitRoom(x);

                    return unitRoom == engineerRoom;
                });
            
                foreach (var catUnit in catUnitsInDifferentRoom)
                {
                    catUnit.UnitGameObject().GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            
                foreach (var catUnit in catUnitsInSameRoom)
                {
                    catUnit.UnitGameObject().GetComponentInChildren<SpriteRenderer>().enabled = true;
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
            }
        }
    }
}