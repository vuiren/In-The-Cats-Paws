using Game_Code.Factories;
using Game_Code.Managers;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Players;
using Game_Code.Network.Syncs;

namespace Game_Code.Controllers
{
	public class PlayersWorker
	{
		private readonly StaticData _staticData;
		private readonly PlayerFactory _playerFactory;

		private readonly CurrentPlayerManager _currentPlayerManager;
		private readonly INetworkPlayersSync _playersSync;
		
		public PlayersWorker(INetworkPlayersSync playersSync, StaticData staticData,
			PlayerFactory playerFactory, CurrentPlayerManager currentPlayerManager)
		{
			_playersSync = playersSync;
			_staticData = staticData;
			_playerFactory = playerFactory;
			_currentPlayerManager = currentPlayerManager;
		}

		public void InitializePlayer()
		{
			if (_currentPlayerManager.CurrentPlayerType == PlayerType.Engineer)
			{
				var playerEngineer = _playerFactory
					.CreatePlayer<PlayerEngineer>(_staticData.playerEngineerPrefab.name);
				_playersSync.RegisterPlayer(playerEngineer);
			}
			else
			{
				var playerSmartCat = _playerFactory
					.CreatePlayer<PlayerSmartCat>(_staticData.playerSmartCatPrefab.name);
				_playersSync.RegisterPlayer(playerSmartCat);
			}

			_playersSync.RefreshPlayers();
		}
	}
}