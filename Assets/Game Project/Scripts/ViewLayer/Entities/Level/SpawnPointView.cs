using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
	public class SpawnPointView : Entity<SpawnPoint>
	{
		[SerializeField] private Vector2Int room;
		[SerializeField] private UnitType spawnUnitUnitType;
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private int spawnID;
		
		public Transform SpawnPointTransform => spawnPoint;
		public int SpawnID => spawnID;
		
		protected override void SetModel()
		{
			model = new SpawnPoint
			{
				Room = room,
				UnitType = spawnUnitUnitType,
				GameObjectLink = gameObject
			};
		}
	}
}
