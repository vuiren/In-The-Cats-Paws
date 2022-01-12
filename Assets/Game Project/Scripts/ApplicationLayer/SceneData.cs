using Game_Project.Scripts.ApplicationLayer.Controllers.Input;
using Game_Project.Scripts.NetworkLayer.Services;
using Game_Project.Scripts.ViewLayer;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer
{
	public sealed class SceneData : MonoBehaviour
	{
		public GameObject catTurnUI, engineerTurnUI;
		
		public MouseInput mouseInput;
		public TouchInput touchInput;
		public CameraController cameraController;
		public AudioSource backgroundMusic;
		public GameManager gameManager;

		#region Network

		public NetworkRoomsService networkRoomsService;
		public NetworkChatService networkChatService;
		public NetworkPlayersService playersService;
		public NetworkCorridorsService corridorsService;
		public NetworkTurnsService turnsService;
		public NetworkUnitsService unitsService;
		public NetworkRepairPointsService repairPointsService;
		public NetworkUnitExplosionService networkUnitExplosionService;
		public NetworkWinService networkWinService;

		#endregion

	}
}