using System;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.LogicLayer.Services;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.UI
{
	public class GameCovering : MonoBehaviour
	{
		[SerializeField] private GameObject cover;
		private ICurrentPlayerService _currentPlayerService;
		private ITurnService _turnService;
		
		[Inject]
		public void Construct(IGameStatusService gameStatusService,
			ICurrentPlayerService currentPlayerService, ITurnService turnService)
		{
			_currentPlayerService = currentPlayerService;
			_turnService = turnService;

			SubscribeCovering();
		}

		protected void Start()
		{
			var player = _currentPlayerService.CurrentPlayerType();
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
			var player = _currentPlayerService.CurrentPlayerType();

			switch (player)
			{
				case PlayerType.Engineer:
					break;
				case PlayerType.SmartCat:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void CoverScreen() { cover.SetActive(true); }
		private void HideCover() { cover.SetActive(false); }
	}
}
