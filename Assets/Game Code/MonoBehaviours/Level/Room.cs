using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Level
{
	public class Room : MonoBehaviour
	{
		[SerializeField] private List<Corridor> corridors;
		[SerializeField] private List<Button> buttons;
		[SerializeField] private Transform pointsForUnitsParent;

		private ILogger _logger;

		public IEnumerable<Vector3> GetFreePoints()
		{
			var result = new List<Vector3>();
			for (var i = 0; i < pointsForUnitsParent.childCount; i++)
			{
				var child = pointsForUnitsParent.GetChild(i);
				result.Add(child.position);
			}

			return result;
		}
		
		public bool roomEnabled;

		[Inject]
		public void Construct(ILogger logger)
		{
			_logger = logger;
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
			Debug.Log($"Done enabling Room {gameObject.name}");
		}

		public void HideRoom(bool hideCorridors, bool hideButtons)
		{
			_logger.Log(this, $"Disabling Room {gameObject.name}");

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
			_logger.Log(this, $"Done disabling Room {gameObject.name}");
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
