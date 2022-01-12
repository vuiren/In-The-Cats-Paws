using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game_Project.Scripts.ApplicationLayer.Controllers;
using Game_Project.Scripts.ApplicationLayer.Controllers.Drawers;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using Game_Project.Scripts.ViewLayer.Data;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Factories;
using UnityEngine;
using Zenject;
using Button = Game_Project.Scripts.DataLayer.Level.Button;

namespace Game_Project.Scripts.ApplicationLayer
{
	public sealed class GameManager : MonoBehaviour
	{
		private IMyLogger _logger;
		private IRoomsService _roomsService;
		private ICorridorsService _corridorsService;
		private IButtonsService _buttonsService;
		private IRepairPointsService _repairPointsService;
		private IExitPointsService _exitPointsService;
		private ISpawnPointsService _spawnPointsService;
		private IPlayersService _playersService;
		private IGameStatusService _gameStatusService;
		private ICurrentPlayerService _currentPlayerService;
		private IUnitFactory _unitFactory;
		private StaticData _staticData;
		private IUnitsService _unitsService;
		private IMapDrawer _mapDrawer;
		private TurnUIController _turnUIController;
		private IUnitsSelectionService _selectionService;
		private AudioSource _backgroundMusic;

			[Inject]
		public void Construct(IRoomsService roomsService,
			ICorridorsService corridorsService, IButtonsService buttonsService, IRepairPointsService repairPointsService,
			IExitPointsService exitPointsService, ISpawnPointsService spawnPointsService, IUnitFactory unitFactory,
			StaticData staticData, IPlayersService playersService, IGameStatusService gameStatusService, 
			ICurrentPlayerService currentPlayerService, IUnitsService unitsService, IMapDrawer mapDrawer,
			TurnUIController turnUIController, IUnitsSelectionService selectionService, AudioSource backgroundMusic)
		{
			_logger = LoggerFactory.Create(this);
			_selectionService = selectionService;
			_turnUIController = turnUIController;
			_roomsService = roomsService;
			_corridorsService = corridorsService;
			_buttonsService = buttonsService;
			_repairPointsService = repairPointsService;
			_exitPointsService = exitPointsService;
			_spawnPointsService = spawnPointsService;
			_unitFactory = unitFactory;
			_staticData = staticData;
			_playersService = playersService;
			_gameStatusService = gameStatusService;
			_currentPlayerService = currentPlayerService;
			_unitsService = unitsService;
			_mapDrawer = mapDrawer;
			_backgroundMusic = backgroundMusic;
		}

		private async void Start()
		{
			AddEntitiesToSystems();
			CreatePlayerForUser();
			CreateUnitsForPlayer();
			SetPlayerReady();
			await WaitForBothPlayersReady();
			RegisterAllUnits();
			SetStartVisibility();
			SetStartUI();
			SetStartSelectedUnit();
			StartGame();
			await StartBackgroundMusicWithDelay();
		}

		private async Task StartBackgroundMusicWithDelay()
		{
			await Task.Delay(3000);
			_backgroundMusic.Play();
		}

		private void SetStartSelectedUnit()
		{
			if (_currentPlayerService.CurrentPlayerType() == PlayerType.Engineer)
			{
				var engineer = _unitsService.GetUnitsByUnitType(UnitType.Engineer).First();
				_selectionService.SelectUnit(engineer.ID);
			}
		}

		private void SetStartUI()
		{
			_turnUIController.RedrawUI(Turn.Engineer);
		}


		private void RegisterAllUnits()
		{
			var units = EntitiyIDsSetter.GetEntities<Unit>();

			foreach (var unit in units)
			{
				unit.model = _unitsService.GetUnitByID(unit.model.ID);
			}
		}

		private void SetPlayerReady()
		{
			_playersService.PlayerReady(_currentPlayerService.CurrentPlayerType());
		}

		private void CreateUnitsForPlayer()
		{
			var engineerTypes = new[] {UnitType.Engineer};
			var smartCatTypes = new[] {UnitType.CatBotBiter, UnitType.CatBotBomb, UnitType.CatBotButtonPusher};

			var searchTypes = _currentPlayerService.CurrentPlayerType() == PlayerType.Engineer
				? engineerTypes
				: smartCatTypes;
			
			var spawnPoints = _spawnPointsService.GetAll();

			foreach (var spawnPoint in spawnPoints)
			{
				if(!searchTypes.Contains(spawnPoint.UnitType)) continue;

				var prefab = _staticData.unitTypeToPrefabs
					.Single(x => x.unitType == spawnPoint.UnitType)
					.prefab;
                
				_unitFactory.CreateUnit(prefab, spawnPoint.GameObjectLink.GetComponent<SpawnPointView>());
			}
		}

		private void AddEntitiesToSystems()
		{
			var rooms = EntitiyIDsSetter.GetEntities<Room>();
			foreach (var room in rooms)
			{
				_roomsService.RegisterRoom(room.model);
			}

			var corridors = EntitiyIDsSetter.GetEntities<Corridor>();
			foreach (var corridor in corridors)
			{
				_corridorsService.RegisterCorridor(corridor.model);
			}

			var buttons = EntitiyIDsSetter.GetEntities<Button>();
			foreach (var button in buttons)
			{
				_buttonsService.RegisterButton(button.model);
			}

			var repairPoints = EntitiyIDsSetter.GetEntities<RepairPoint>();
			foreach (var repairPoint in repairPoints)
			{
				_repairPointsService.RegisterRepairPoint(repairPoint.model);
			}

			var exitPoints = EntitiyIDsSetter.GetEntities<ExitPoint>();
			foreach (var exitPoint in exitPoints)
			{
				_exitPointsService.RegisterExitPoint(exitPoint.model);
			}

			var spawnPoints = EntitiyIDsSetter.GetEntities<SpawnPoint>();
			foreach (var spawnPoint in spawnPoints)
			{
				_spawnPointsService.RegisterSpawnPoint(spawnPoint.model);
			}
		}
		
		private async UniTask WaitForBothPlayersReady()
		{
			while (_playersService.GetAll().Count(x => x.IsReady) < _staticData.playersCount)
			{
				await Task.Yield();
			}
		}

		private void StartGame()
		{
			_gameStatusService.SetGameStatus(GameStatus.Going);
		}

		private void SetStartVisibility()
		{
			_mapDrawer.ReDrawMap();
		}
		
		private void CreatePlayerForUser()
		{
			_logger.Log("Registering player for user");
			var playerType = _currentPlayerService.CurrentPlayerType();
			_playersService.RegisterPlayer(playerType);
			_logger.Log("Done registering player for user");
		}
	}
}
