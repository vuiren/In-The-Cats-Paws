using Game_Code.MonoBehaviours.Players;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Input
{
	[RequireComponent(typeof(PhotonView))]
	public abstract class PlayerInput : MonoBehaviour
	{
		[SerializeField] private bool isEngineer, isSmartCat;

		protected Player Player;
		private PhotonView _photonView;
		private ITurnService _turnService;
		
		protected void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		public void Init(Player player, ITurnService turnService)
		{
			Player = player;
			_turnService = turnService;
			isEngineer = player is PlayerEngineer;
			isSmartCat = player is PlayerSmartCat;
		}

		protected void Update()
		{
			if (!_photonView.IsMine) return;
			var turn = _turnService.CurrentTurn();
				
			if (isEngineer && turn == Turn.Engineer)
				ProcessInput();

			if (isSmartCat && turn == Turn.SmartCat)
				ProcessInput();
		}

		protected abstract void ProcessInput();
	}
}