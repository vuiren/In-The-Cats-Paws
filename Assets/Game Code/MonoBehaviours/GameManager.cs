using System;
using Game_Code.Controllers;
using Game_Code.Managers;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours
{
	public class GameManager : MonoBehaviour
	{
		private PlayersWorker _playersWorker;
		private CurrentPlayerManager _currentPlayerManager;
		private IRoomsVisibilityService _roomsVisibilityService;
		private ILogger _logger;

		[Inject]
		public void Construct(ILogger logger, INetworkTurnsSync networkTurnsSync, PlayersWorker playersWorker,
			CurrentPlayerManager currentPlayerManager, IRoomsVisibilityService roomsVisibilityService)
		{
			_playersWorker = playersWorker;
			_logger = logger;
			_currentPlayerManager = currentPlayerManager;
			_roomsVisibilityService = roomsVisibilityService;
		}

		private void Start()
		{
			SetStartVisibility();
			CreatePlayerForUser();
		}

		private void SetStartVisibility()
		{
			switch (_currentPlayerManager.CurrentPlayerType)
			{
				case PlayerType.Engineer:
					_roomsVisibilityService.HideAllRooms(true,true);
					break;
				case PlayerType.SmartCat:
					_roomsVisibilityService.HideAllRooms(true,true);
					_roomsVisibilityService.ShowAllRooms(true,false);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void CreatePlayerForUser()
		{
			_logger.Log(this,"Creating player for user");
			_playersWorker.InitializePlayer();
			_logger.Log(this,"Done creating player for user");
		}
	}
}
