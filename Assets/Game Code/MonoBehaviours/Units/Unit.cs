using System;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Level;
using Game_Code.Network;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Units
{
	[RequireComponent(typeof(PhotonView))]
	public class Unit : MonoBehaviour
	{
		public UnitType unitType;
		
		private float _moveSpeed;
		private Vector3 _targetPos;
		
		private bool _initialized = false;

		private INetworkTurnsSync _networkTurnsSync;
		private ILogger _logger;
		private IUnitRoomService _unitRoomService;
		
		public Action OnUnitChose { get; set; }
		public Action OnUnitUnchoose { get; set; }

		[Inject]
		public void Construct(ILogger logger,
			StaticData staticData, IRoomsService roomsService, IUnitsService unitsService,
			INetworkTurnsSync networkTurnsSync, IUnitRoomService unitRoomService)
		{
			_moveSpeed = staticData.unitMoveSpeed;
			_targetPos = transform.position;
			_networkTurnsSync = networkTurnsSync;
			_logger = logger;
			_unitRoomService = unitRoomService;
			_initialized = true;
			logger.Log($"Created Unit {gameObject.name}");
		}

		protected virtual void Update()
		{
			if (_initialized)
				Move();
		}

		private void Move()
		{
			var currentRoom = _unitRoomService.FindUnitRoom(this);
			if (!currentRoom) return;
			transform.position = Vector2.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);
		}

		public void ChooseUnit()
		{
			OnUnitChose?.Invoke();
		}

		public void UnchooseUnit()
		{
			OnUnitUnchoose?.Invoke();
		}

		public void RefreshTargetPos()
		{
			var room = _unitRoomService.FindUnitRoom(this);
			if(!room) return;
			
			_targetPos = room.GetPointForUnit();
			room.FreePointRPC(_targetPos);
		}
	}
}
