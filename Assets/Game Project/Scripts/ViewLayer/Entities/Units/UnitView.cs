using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Data;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.Entities.Units
{
	public class UnitView : Entity<Unit>
	{
		private float _moveSpeed;
		[SerializeField] private bool initialized;
		[SerializeField] private GameObject drawModel;
		
		[Inject]
		public void Construct(Unit unit, StaticData staticData)
		{
			base.Construct();
			_moveSpeed = staticData.unitMoveSpeed;
			model = unit;
			initialized = true;
		}

		protected virtual void Update()
		{
			if (initialized)
				Move();
		}

		private void Move()
		{
			transform.position = Vector2.MoveTowards(transform.position, 
				model.Position, _moveSpeed * Time.deltaTime);
		}

		public void DrawUnit(bool draw)
		{
			drawModel.SetActive(draw);
		}
		
		protected override void SetModel()
		{
			
		}
	}
}
