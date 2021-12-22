using System;
using Game_Code.MonoBehaviours.Data;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Units
{
	public interface IUnit
	{
		/*void SelectUnit();
		void DeselectUnit();*/
		GameObject UnitGameObject();
		UnitType UnitType();
		void SetTargetPointForUnit(Vector3 point);
	}
	
	public class Unit : MonoBehaviour, IUnit
	{
		public UnitType unitType;
		
		private float _moveSpeed;
		private Vector3 _targetPos;
		
		private bool _initialized;

		protected ILogger Logger;
		protected ITurnService TurnService;
		protected INetworkTurnsSync TurnsSync;
		
		/*public Action OnUnitSelect { get; set; }
		public Action OnUnitDeselect { get; set; }*/

		[Inject]
		public virtual void Construct(ILogger logger, StaticData staticData, IRoomsService roomsService, 
			IUnitsService unitsService, ITurnService turnService, INetworkTurnsSync networkTurnsSync)
		{
			_moveSpeed = staticData.unitMoveSpeed;
			_targetPos = transform.position;
			_initialized = true;
			TurnService = turnService;
			Logger = logger;
			TurnsSync = networkTurnsSync;
			logger.Log($"Created Unit {gameObject.name}");
		}

		protected virtual void Update()
		{
			if (_initialized)
				Move();
		}

		private void Move()
		{
			transform.position = Vector2.MoveTowards(transform.position, 
				_targetPos, _moveSpeed * Time.deltaTime);
		}
		
		/*public virtual void SelectUnit() => OnUnitSelect?.Invoke();
		public virtual void DeselectUnit() => OnUnitDeselect?.Invoke();*/
		public GameObject UnitGameObject() => gameObject;
		public UnitType UnitType() => unitType;

		public void SetTargetPointForUnit(Vector3 point) => _targetPos = point;
	}
}
