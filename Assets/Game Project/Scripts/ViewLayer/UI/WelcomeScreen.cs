using System;
using System.Linq;
using DG.Tweening;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.UI
{
	class WelcomeScreen: MonoBehaviour
	{
		[SerializeField] private GameObject welcomeScreen, catImage, engineerImage, youCatText, youEngineerText;
		[SerializeField] private Transform catImagePosition, engineerImagePosition;
		[SerializeField] private Text statusText;

		private IPlayersService _playersService;
		private IMyLogger _logger;
		
		[Inject]
		public void Construct(IPlayersService playersService, IGameStatusService gameStatusService, 
			ICurrentPlayerService currentPlayerService)
		{
			_playersService = playersService;
			_logger = LoggerFactory.Create(this);
			_playersService.OnPlayerRegistered(ShowPlayerImage);

			InitUI(currentPlayerService.CurrentPlayerType());
			
			var playerEngineer = _playersService.GetAll().FirstOrDefault(x=>x.PlayerType == PlayerType.Engineer);
			if (playerEngineer != null)
			{
				ShowPlayerImage(playerEngineer);
			}

			var smartCat = _playersService.GetAll().FirstOrDefault(x=>x.PlayerType == PlayerType.SmartCat );
			if (smartCat != null)
			{
				ShowPlayerImage(smartCat);
			}
			
			gameStatusService.RegisterForGameStart(HideWelcomeScreen);
		}

		private void ShowPlayerImage(Player obj)
		{
			switch (obj.PlayerType)
			{
				case PlayerType.Engineer:
					_logger.Log("Showing image of engineer");
					engineerImage.SetActive(true);
					engineerImage.transform.DOMove(engineerImagePosition.position, 1f);
					break;
				case PlayerType.SmartCat:
					_logger.Log("Showing image of smart cat");
					catImage.SetActive(true);
					catImage.transform.DOMove(catImagePosition.position, 1f);
					break;
			}
		}

		private void InitUI(PlayerType playerType)
		{
			catImage.SetActive(false);
			engineerImage.SetActive(false);
			youCatText.SetActive(false);
			youEngineerText.SetActive(false);

			switch (playerType)
			{
				case PlayerType.Engineer:
					youEngineerText.SetActive(true);
					break;
				case PlayerType.SmartCat:
					youCatText.SetActive(true);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
			}
		}

		private void HideWelcomeScreen()
		{
			statusText.DOText("3", 1f).OnComplete(() =>
			{
				statusText.DOText("2", 1f).OnComplete(() =>
				{
					statusText.DOText("1", 1f).OnComplete(() =>
					{
						statusText.DOText("Ready", 1f).OnComplete(() =>
						{
							statusText.DOText("", 1f).OnComplete(() => welcomeScreen.SetActive(false));
						});
					});
				});

			});
		}
	}
}
