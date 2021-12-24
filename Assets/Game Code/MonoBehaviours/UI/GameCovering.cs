using System;
using Game_Code.Managers;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.UI
{
	public class GameCovering : MonoBehaviour
	{
		[SerializeField] private GameObject cover;
		private CurrentPlayerManager _currentPlayerManager;
		private ITurnService _turnService;
		
		[Inject]
		public void Construct(IGameStatusService gameStatusService,
			CurrentPlayerManager currentPlayerManager, ITurnService turnService)
		{
			_currentPlayerManager = currentPlayerManager;
			_turnService = turnService;

			SubscribeCovering();
		}

		protected void Start()
		{
			var player = _currentPlayerManager.CurrentPlayerType;
			var turn = _turnService.CurrentTurn();
			
			switch (player)
			{
				case PlayerType.Engineer:
					cover.SetActive(turn != Turn.Engineer);
					break;
				case PlayerType.SmartCat:
					cover.SetActive(turn != Turn.SmartCat);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SubscribeCovering()
		{
			var player = _currentPlayerManager.CurrentPlayerType;

			switch (player)
			{
				case PlayerType.Engineer:
					_turnService.OnEngineerTurn(HideCover);
					_turnService.OnSmartCatTurn(CoverScreen);
					break;
				case PlayerType.SmartCat:
					_turnService.OnEngineerTurn(CoverScreen);
					_turnService.OnSmartCatTurn(HideCover);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void CoverScreen() { cover.SetActive(true); }
		private void HideCover() { cover.SetActive(false); }
	}
}
