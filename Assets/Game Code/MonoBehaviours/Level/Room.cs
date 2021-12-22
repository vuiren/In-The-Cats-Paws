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
		private ILogger _logger;

		public Vector3[] GetFreePoints()
		{
			var result = new List<Vector3>();
			for (var i = 0; i < pointsForUnitsParent.childCount; i++)
			{
				var child = pointsForUnitsParent.GetChild(i);
				result.Add(child.position);
				_pointsForUnitsQueue.Enqueue(child.position);
			}

			return result.ToArray();
		}
		
		public bool roomEnabled = false;

		[Inject]
		public void Construct(ILogger logger)
		{
			_logger = logger;
		}

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
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

		public void DrawRoom(bool drawCorridors, bool drawButtons)
		{
			Debug.Log($"Enabling Room {gameObject.name}");

			if (drawCorridors)
			{
				foreach (var e in corridors)
				{
					e.Door.gameObject.SetActive(true);
				}
			}

			if (drawButtons)
			{
				foreach (var e in buttons)
				{
					e.TogglePressButton(true);
				}
			}

			roomEnabled = true;
		}

		public void HideRoom(bool hideCorridors, bool hideButtons)
		{
			_logger.Log($"Disabling Room {gameObject.name}");

			if (hideCorridors)
			{
				foreach (var e in corridors)
				{
					e.Door.gameObject.SetActive(false);
				}
			}

			if (hideButtons)
			{
				foreach (var e in buttons)
				{
					e.TogglePressButton(false);
				}
			}

			roomEnabled = false;
		}

		public IEnumerable<Room> GetAvailableRooms()
		{
			var result = new List<Room>();
			foreach (var corridor in corridors.Where(corridor => !corridor.Door.Closed))
			{
				result.AddRange(corridor.ConnectedRooms.Where(x => x != this));
			}

			return result;
		}
	}
}
