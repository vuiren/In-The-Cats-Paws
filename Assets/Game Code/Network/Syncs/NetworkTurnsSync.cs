using System;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
	public interface INetworkTurnsSync
	{
		void EndCurrentTurn();
	}
	
	[RequireComponent(typeof(PhotonView))]
	public class NetworkTurnsSync : MonoBehaviour, INetworkTurnsSync
	{
		private PhotonView _photonView;
		private ILogger _logger;
		private ITurnService _turnService;

		[Inject]
		public void Construct(ILogger logger, ITurnService turnService)
		{
			_logger = logger;
			_photonView = GetComponent<PhotonView>();
			_turnService = turnService;
		}

		[PunRPC]
		private void EndCurrentTurnRPC()
		{
			_turnService.EndCurrentTurn();
			var turn = _turnService.CurrentTurn();

			switch (turn)
			{
				case Turn.Engineer:
					_logger.Log("Now it's an engineer step");
					break;
				case Turn.SmartCat:
					_logger.Log("Now it's a smart cat's step");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void EndCurrentTurn()
		{
			_photonView.RPC("EndCurrentTurnRPC", RpcTarget.All);
		}
	}
}