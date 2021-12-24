using Game_Code.MonoBehaviours.Data.Databases;
using Game_Code.MonoBehaviours.Level;
using Game_Code.Network.Syncs;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Data
{
	public class SceneData : MonoBehaviour
	{
		public CameraController cameraController;
		public GameManager gameManager;
		public SpawnPoint[] spawnPoints;
		public RepairPoint[] repairPoints;
		public Database database;

		#region Network
		public NetworkTurnsSync networkTurnsSync;
		public NetworkPlayersSync networkPlayersSync;
		public NetworkRoomsSync networkRoomsSync;
		public NetworkUnitsSync networkUnitsSync;
		public NetworkDoorsSync networkDoorsSync;
		public NetworkFreeRoomPointsSync networkFreeRoomPointsSync;
		public NetworkCatBombExplosionSync networkCatBombExplosionSync;
		public NetworkGameSync networkGameSync;
		public NetworkRepairSync repairSync;
		#endregion

	}
}