using Game_Project.Scripts.ApplicationLayer.Controllers;
using Game_Project.Scripts.ApplicationLayer.Controllers.Drawers;
using Game_Project.Scripts.ApplicationLayer.Controllers.UnitControllers;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.LogicLayer.Interfaces;
using QFSW.QC;
using UnityEngine;
using Zenject;
// ReSharper disable ExplicitCallerInfoArgument

namespace Game_Project.Scripts.ApplicationLayer
{
    public class CommandsExecutor:MonoBehaviour
    {
        private IMyLogger _logger;
        private IRoomsService _roomsService;
        private ICorridorsService _corridorsService;
        private IButtonsService _buttonsService;
        private IRepairPointsService _repairPointsService;
        private IExitPointsService _exitPointsService;
        private ISpawnPointsService _spawnPointsService;
        private IUnitsService _unitsService;
        private ITurnService _turnService;
        private IPlayersService _playersService;
        private IGameStatusService _gameStatusService;
        private UnitMovingController _unitMovingController;
        private EngineerRepairPointFixer _engineerRepairPointFixer;
        private UnitsExplosionController _explosionController;
        private IUnitExplosionService _explosionService;
        private EffectsSpawner _effectsSpawner;
        private InputWorker _inputWorker;
        private EngineerMapDrawer _engineerMapDrawer;
        private CatMapDrawer _catMapDrawer;
        private IMapDrawer _mapDrawer;
        private ICurrentPlayerService _currentPlayerService;
        private IWinService _winService;
        
        [Inject]
        public void Construct(IRoomsService roomsService, 
            ICorridorsService corridorsService, IButtonsService buttonsService, IRepairPointsService repairPointsService,
            IExitPointsService exitPointsService, ISpawnPointsService spawnPointsService, IUnitsService unitsService,
            ITurnService turnService, IPlayersService playersService, IGameStatusService gameStatusService,
            UnitMovingController unitMovingController, EngineerRepairPointFixer engineerRepairPointFixer,
            UnitsExplosionController explosionController, IUnitExplosionService explosionService,
            EffectsSpawner effectsSpawner, InputWorker inputWorker, EngineerMapDrawer engineerMapDrawer, 
            CatMapDrawer catMapDrawer, IMapDrawer mapDrawer, ICurrentPlayerService currentPlayerService, 
            IWinService winService)
        {
            _logger = LoggerFactory.Create(this);
            _roomsService = roomsService;
            _currentPlayerService = currentPlayerService;
            _engineerMapDrawer = engineerMapDrawer;
            _corridorsService = corridorsService;
            _buttonsService = buttonsService;
            _repairPointsService = repairPointsService;
            _exitPointsService = exitPointsService;
            _spawnPointsService = spawnPointsService;
            _unitsService = unitsService;
            _turnService = turnService;
            _playersService = playersService;
            _gameStatusService = gameStatusService;
            _unitMovingController = unitMovingController;
            _engineerRepairPointFixer = engineerRepairPointFixer;
            _explosionController = explosionController;
            _explosionService = explosionService;
            _effectsSpawner = effectsSpawner;
            _inputWorker = inputWorker;
            _catMapDrawer = catMapDrawer;
            _mapDrawer = mapDrawer;
            _winService = winService;
        }

        [Command("game.units.biteEngineer")]
        private void BiteEngineer()
        {
            _turnService.AddEngineersSkippingTurn();
        }

        [Command("game.win.engineer")]
        private void EngineerWin()
        {
            _winService.EngineerWin();
        }

        [Command("game.win.smartCat")]
        private void SmartCatWin()
        {
            _winService.CatWin();
        }
        
        #region Controllers

        [Command("game.drawer.drawForEngineer")]
        private void DrawMapForEngineer()
        {
            _engineerMapDrawer.ReDrawMap();
        }

        [Command("game.drawer.drawForSmartCat")]
        private void DrawForSmartCat()
        {
            _catMapDrawer.ReDrawMap();
        }

        [Command("game.drawer.drawForPlayer")]
        private void DrawForCurrentPlayer()
        {
            _mapDrawer.ReDrawMap();
        }

        [Command("game.input.imitateClick")]
        private void ImitateClick(Vector3 clickPos)
        {
            _inputWorker.MakeTurn(clickPos);
        }
        
        
        [Command("game.units.sendToRoom")]
        private void TryToSendUnitToRoom(int unitId, Vector2Int room)
        {
            _unitMovingController.TryToSendUnitToRoom(unitId, room);
        }

        [Command("game.repairPoint.fix")]
        private void FixRepairPoint(int unitId, Vector2Int room)
        {
            _engineerRepairPointFixer.TryToFixRepairPointAtRoom(unitId, room);
        }

        [Command("game.effects.createExplosionEffect")]
        private void CreateExplosionEffect(int unitId)
        {
            _effectsSpawner.CreateExplosionEffect(unitId);
        }

        #region Explosion
        [Command("game.explosion.explodeUnit")]
        private void ExplodeUnit(int unitId, int turnsCount)
        {
            _explosionController.ExplodeUnit(unitId, turnsCount);
        }

        [Command("game.explosion.tickTimer")]
        private void TickExplosionTimer()
        {
            _explosionController.TickExplosionService();
        }

        [Command("game.explosion.turnLeftForUnit")]
        private void ShowTurnsLeftUntilUnitExplodes(int unitId)
        {
            var turns = _explosionService.TurnLeftUntilUnitExplode(unitId);
            if (turns != -1)
            {
                _logger.Log($"{turns} turns left until unit with id {unitId} explodes");
            }
        }
        
        
        [Command("game.explosion.showAll")]
        private void ShowAllExplosionTimers()
        {
            var turns = _explosionService.GetAll();
            foreach (var tuple in turns)
            {
                _logger.Log($"{tuple.Item2} turns left until unit with id {tuple.Item1} explodes");
            }
        }
        

        #endregion
        #endregion

        #region Rooms

        [Command("game.rooms.getAll")]
        private void GetAllRooms()
        {
            var rooms = _roomsService.GetAll();

            foreach (var room in rooms)
            {
                _logger.Log(room.ToString());
            }
        }

        #endregion
        
        #region GameStatus

        [Command("game.status")]
        private void GetGameStatus()
        {
            var status = _gameStatusService.GetGameStatus();
            _logger.Log($"Game status: {status}");
        }

        #endregion

        #region Players

        [Command("game.players.getAll")]
        private void GetAllPlayers()
        {
            var players = _playersService.GetAll();
            foreach (var playerType in players)
            {
                _logger.Log($"Player type of {playerType}");
            }
        }

        [Command("game.players.getCurrent")]
        private void GetCurrentPlayer()
        {
            _logger.Log($"Current player: {_currentPlayerService.CurrentPlayerType()}");
        }
        #endregion
        
        #region Turns

        [Command("game.turns.getCurrent")]
        private void GetCurrentTurn()
        {
            var turn = _turnService.CurrentTurn();
            _logger.Log($"Current turn: {turn}");
        }

        [Command("game.turns.endCurrent")]
        private void EndCurrentTurn()
        {
            _turnService.EndCurrentTurn();    
        }
        
        #endregion

        #region Units
        [Command("game.units.getAll")]
        private void GetAllUnits()
        {
            var units = _unitsService.GetAll();

            foreach (var unit in units)
            {
                _logger.Log(unit.ToString());
            }
        }

        [Command("game.units.unitGoToRoom")]
        private void UnitGoToRoom(int unitId, Vector2Int roomCoords)
        {
            _unitsService.UnitGoToRoom(unitId, roomCoords);
        }
        #endregion

        #region SpawnPoints

        [Command("game.spawnPoints.getAll")]
        private void GetAllSpawnPoints()
        {
            var spawnPoints = _spawnPointsService.GetAll();

            foreach (var spawnPoint in spawnPoints)
            {
                _logger.Log(spawnPoint.ToString());
            }
        }

        [Command("game.spawnPoints.get")]
        private void GetSpawnPoint(Vector2Int room)
        {
            var spawnPoint = _spawnPointsService.GetSpawnPointInRoom(room);
            
            if(spawnPoint == null) return;
            
            _logger.Log(spawnPoint.ToString());
        }
        
        #endregion

        #region ExitPoint

        [Command("game.exitPoint.get")]
        private void GetExitPoint()
        {
           _logger.Log(_exitPointsService.GetExitPoint().ToString());
        }

        #endregion
        
        #region RepairPoints
        [Command("game.repairPoints.showAll")]
        private void ShowAllRepairPoints()
        {
            var repairPoints = _repairPointsService.GetAll();

            foreach (var button in repairPoints)
            {
                _logger.Log(button.ToString());
            }
        }

        [Command("game.repairPoints.get")]
        private void GetRepairPoint(Vector2Int room)
        {
            var repairPoint = _repairPointsService.GetRepaintPointInRoom(room);
            if (repairPoint == null) return;
            
            _logger.Log(repairPoint.ToString());
        }
        #endregion

        #region Corridors

        [Command("game.corridors.showAll")]
        private void ShowAllRegisteredCorridors()
        {
            var corridors = _corridorsService.GetAll();
            foreach (var corridor in corridors)
            {
                _logger.Log(corridor.ToString());
            }
        }

        [Command("game.corriods.lock")]
        private void LockCorridor(Vector2Int room1, Vector2Int room2, bool doLock)
        {
            _corridorsService.LockCorridor(room1, room2, doLock);
        }

        [Command("game.corridors.getcorridor")]
        private void GetCorridor(Vector2Int room1, Vector2Int room2)
        {
            var corridor = _corridorsService.GetCorridor(room1, room2);
            if (corridor != null)
            {
                _logger.Log(corridor.ToString());
            }
        }

        #endregion

        #region Buttons
        [Command("game.buttons.showAll")]
        private void ShowAllButtons()
        {
            var buttons = _buttonsService.GetAll();

            foreach (var button in buttons)
            {
                _logger.Log(button.ToString());
            }
        }

        [Command("game.buttons.get")]
        private void GetButton(Vector2Int buttonRoom, Vector2Int room1, Vector2Int room2)
        {
            var button = _buttonsService.GetButton(buttonRoom, room1, room2);
            if (button == null) return;
            
            _logger.Log(button.ToString());
        }

        #endregion
        
        [Command]
        private void ShowAllRegisteredRooms()
        {
            var rooms = _roomsService.GetAll();

            foreach (var room in rooms)
            {
                _logger.Log(room.ToString());
            }
        }
    }
}