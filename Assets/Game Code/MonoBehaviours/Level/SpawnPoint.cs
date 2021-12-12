using Game_Code.MonoBehaviours.Units;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Level
{
	public class SpawnPoint : MonoBehaviour
	{
		[SerializeField] private UnitType spawnUnitUnitType;
		[SerializeField] private Room spawnRoom;

		public Transform SpawnPointTransform => spawnRoom.transform;
		public Room SpawnRoom => spawnRoom;
		public UnitType SpawnUnitType => spawnUnitUnitType;
	}
}
