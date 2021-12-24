using System;
using Game_Code.Services;
using Photon.Pun;
using QFSW.QC;
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
			_logger.Log(this, "Ending current step");
			_turnService.EndCurrentTurn();
			var turn = _turnService.CurrentTurn();

			switch (turn)
			{
				case Turn.Engineer:
					_logger.Log(this,"Now it's an engineer step");
					break;
				case Turn.SmartCat:
					_logger.Log(this,"Now it's a smart cat's step");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			_logger.Log(this, "Done ending current step");
		}

		[Command("network.endturn")]
		public void EndCurrentTurn()
		{
			_photonView.RPC("EndCurrentTurnRPC", RpcTarget.All);
		}
	}
}