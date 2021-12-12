using System;
using Game_Code.Controllers;
using Game_Code.Managers;
using Game_Code.Network;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game_Code.MonoBehaviours
{
	public class GameManager : MonoBehaviour
	{
		public Text stepText;
		private INetworkTurnsSync _networkTurnsSync;
		private PlayersWorker _playersWorker;
		private CurrentPlayerManager _currentPlayerManager;
		private IRoomsVisibilityService _roomsVisibilityService;
		private ILogger _logger;

		[Inject]
		public void Construct(ILogger logger, INetworkTurnsSync networkTurnsSync, PlayersWorker playersWorker,
			CurrentPlayerManager currentPlayerManager, IRoomsVisibilityService roomsVisibilityService)
		{
			_networkTurnsSync = networkTurnsSync;
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
					_roomsVisibilityService.HideAllRooms();
					break;
				case PlayerType.SmartCat:
					_roomsVisibilityService.ShowAllRooms();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void CreatePlayerForUser()
		{
			_logger.Log("Creating player for user");
			_playersWorker.InitializePlayer();
			_logger.Log("Done creating player for user");
		}

		private void Update()
		{
			if(UnityEngine.Input.GetKeyDown(KeyCode.Space) && PhotonNetwork.IsMasterClient)
			{
				_networkTurnsSync.EndCurrentTurn();
			}
		}
	}
}
