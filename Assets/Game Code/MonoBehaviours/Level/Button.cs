using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Level
{
	public class Button : MonoBehaviour
	{
		[SerializeField] private Door door;
		[SerializeField] private GameObject pressButton;

		private INetworkTurnsSync _networkTurnsSync;
		private INetworkDoorsSync _networkDoorsSync;
		private IDoorsService _doorsService;
		
		[Inject]
		public void Construct(INetworkTurnsSync networkTurnsSync, 
			INetworkDoorsSync networkDoorsSync, IDoorsService doorsService)
		{
			_networkTurnsSync = networkTurnsSync;
			_networkDoorsSync = networkDoorsSync;
			_doorsService = doorsService;
		}
		
		public void Press()
		{
			var doorId = _doorsService.GetDoorId(door);
			_networkDoorsSync.SwitchDoorState(doorId);
			_networkTurnsSync.EndCurrentTurn();
		}

		public void TogglePressButton(bool enable)
		{
			pressButton.SetActive(enable);
		}

		private void OnDrawGizmos()
		{
			if (!door) return;
			Gizmos.DrawLine(transform.position, door.transform.position);
		}
	}
}
