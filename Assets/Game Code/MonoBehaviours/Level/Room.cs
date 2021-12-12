using System.Collections.Generic;
using System.Linq;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Level
{
	[RequireComponent(typeof(PhotonView))]
	public class Room : MonoBehaviour
	{
		[SerializeField] private List<Corridor> corridors;
		[SerializeField] private List<Button> buttons;
		[SerializeField] private Transform pointsForUnitsParent;
		
		private PhotonView _photonView;
		private readonly Queue<Vector3> _pointsForUnitsQueue = new();
		private IUnitRoomService _unitRoomService;
		private ILogger _logger;

		public bool roomEnabled = false;

		[Inject]
		public void Construct(ILogger logger, IUnitRoomService unitRoomService)
		{
			_logger = logger;
			_unitRoomService = unitRoomService;
		}
		
		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
			for (var i = 0; i < pointsForUnitsParent.childCount; i++)
			{
				var child = pointsForUnitsParent.GetChild(i);
				_pointsForUnitsQueue.Enqueue(child.position);
			}
		}

		public Vector3 GetPointForUnit()
		{
			if (_pointsForUnitsQueue.Count <= 0) return transform.position;
			
			_photonView.RPC("TakeLastPoint", RpcTarget.Others);
			return _pointsForUnitsQueue.Dequeue();
		}

		private void TakePoint()
		{
			_photonView.RPC("TakePoint", RpcTarget.Others);
			_pointsForUnitsQueue.Dequeue();
		}

		[PunRPC]
		private void TakeLastPoint()
		{
			_pointsForUnitsQueue.Dequeue();
		}

		public void FreePointRPC(Vector3 point)
		{
			_photonView.RPC("FreePoint", RpcTarget.All, point);
		}

		[PunRPC]
		public void FreePoint(Vector3 point)
		{
			_pointsForUnitsQueue.Enqueue(point);
		}
		
		public void AddCorridor(Corridor corridor)
		{
			corridors.Add(corridor);
		}

		public void EnableRoom()
		{
			Debug.Log($"Enabling Room {gameObject.name}");
			foreach (var e in corridors)
			{
				e.Door.gameObject.SetActive(true);
			}

			foreach(var e in buttons)
			{
				e.TogglePressButton(true);
			}

			var unitsInRoom = _unitRoomService.GetAllUnitsInRoom(this);

			foreach(var e in unitsInRoom)
			{
				e.gameObject.SetActive(true);
			}

			roomEnabled = true;
		}

		public void DisableRoom()
		{
			_logger.Log($"Disabling Room {gameObject.name}");

			foreach (var e in corridors)
			{
				e.Door.gameObject.SetActive(false);
			}

			foreach (var e in buttons)
			{
				e.TogglePressButton(false);
			}

			roomEnabled = false;
		}

		public Room[] GetAvailableRooms()
		{
			var result = new List<Room>();
			foreach (var corridor in corridors.Where(corridor => !corridor.Door.Closed))
			{
				result.AddRange(corridor.ConnectedRooms.Where(x=>x!= this));
			}

			return result.ToArray();
		}
	}
}
