using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Project.Scripts.ViewLayer.UI
{
	class WelcomeScreen: MonoBehaviour
	{
		[SerializeField] private GameObject welcomeScreen, catImage, engineerImage, youCatText, youEngineerText;
		[SerializeField] private Transform catImagePosition, engineerImagePosition;
		[SerializeField] private Text statusText;

		private IPlayersService _playersService;
		private ILogger _logger;
		
		/*[Inject]
		public void Construct(ILogger logger, IPlayersService playersService,
			IGameStatusService gameStatusService, ICurrentPlayerService currentPlayerService)
		{
			_playersService = playersService;
			_logger = logger;
			_playersService.OnPlayerAdded(ShowPlayerImage);

			InitUI(currentPlayerService.CurrentPlayerType());
			
			var playerEngineer = _playersService.GetPlayerEngineer();
			if (playerEngineer)
			{
				ShowPlayerImage(playerEngineer);
			}

			var smartCat = _playersService.GetPlayerSmartCat();
			if (smartCat)
			{
				ShowPlayerImage(smartCat);
			}
			
			gameStatusService.RegisterForGameStart(HideWelcomeScreen);
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

		private void ShowPlayerImage(PlayerView obj)
		{
			switch (obj)
			{
				case PlayerViewEngineer:
					_logger.Log("Showing image of engineer");
					engineerImage.SetActive(true);
					engineerImage.transform.DOMove(engineerImagePosition.position, 1f);
					break;
				case PlayerViewSmartCat:
					_logger.Log("Showing image of smart cat");
					catImage.SetActive(true);
					catImage.transform.DOMove(catImagePosition.position, 1f);
					break;
			}
		}*/
	}
}
